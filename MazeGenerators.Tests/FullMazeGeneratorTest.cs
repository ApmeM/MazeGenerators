namespace MazeGenerators.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using MazeGenerators.Common;
    using MazeGenerators.DeadEndRemover;
    using MazeGenerators.RegionConnector;
    using MazeGenerators.RoomGenerator;
    using MazeGenerators.StringParser;
    using MazeGenerators.TreeMazeBuilder;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class FullMazeGeneratorTest
    {
        public class Result : IRoomGeneratorResult, IDeadEndRemoverResult, IRegionConnectorResult, ITreeMazeBuilderResult, IStringParserResult
        {
            public int?[,] Paths { get; set; }

            public List<Vector2> Junctions { get; } = new List<Vector2>();

            public List<Rectangle> Rooms { get; } = new List<Rectangle>();
        }

        public class Settings : IRoomGeneratorSettings, IDeadEndRemoverSettings, IRegionConnectorSettings, ITreeMazeBuilderSettings, IStringParserSettings
        {
            public int Width { get; set; } = 21;

            public int Height { get; set; } = 21;

            public Random Random { get; set; } = new Random();

            public Vector2[] Directions { get; set; } = Utils.Directions.CardinalDirs;

            public int NumRoomTries { get; set; } = 100;

            public bool PreventOverlappedRooms { get; set; } = true;

            public int MinRoomSize { get; set; } = 2;

            public int MaxRoomSize { get; set; } = 5;

            public int MaxWidthHeightRoomSizeDifference { get; set; } = 5;

            public int WindingPercent { get; set; } = 0;

            public int AdditionalPassagesTries { get; set; } = 10;

            public bool RemoveDeadEnds { get; set; } = true;

            public int RoomTileId { get; set; } = 1;

            public int MazeTileId { get; set; } = 1;

            public int JunctionTileId { get; set; } = 1;

            public string MazeText { get; set; }
        }

        public Result Generate(Settings settings)
        {
            var result = new Result();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            CommonAlgorithm.GenerateField(result, settings);
            sw.Stop();
            Console.WriteLine($"GenerateField : {sw.ElapsedMilliseconds}");
            sw.Reset();
            sw.Start();
            RoomGeneratorAlgorithm.GenerateRooms(result, settings);
            sw.Stop();
            Console.WriteLine($"GenerateRooms : {sw.ElapsedMilliseconds}");
            sw.Reset();
            sw.Start();
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings);
            sw.Stop();
            Console.WriteLine($"GrowMaze : {sw.ElapsedMilliseconds}");
            sw.Reset();
            sw.Start();
            RegionConnectorAlgorithm.GenerateConnectors(result, settings);
            sw.Stop();
            Console.WriteLine($"GenerateConnectors : {sw.ElapsedMilliseconds}");
            sw.Reset();
            sw.Start();
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            sw.Stop();
            Console.WriteLine($"RemoveDeadEnds : {sw.ElapsedMilliseconds}");
            return result;
        }

        [Test]
        public void RoomMazeGenerator_Generate_SomeMaze()
        {
            var settings = new Settings
            {
                Width = 21,
                Height = 21,
                Random = new Random(0),
                NumRoomTries = 10
            };

            var result = this.Generate(settings);

            Assert.AreEqual(@"
#####################
# -           #     #
# # ####-## # #     #
# # -     - # #     #
# # ##### # # #     #
# #       # # -     #
# # ####### #########
# # #     #         #
# # #     # ####### #
# - #     # #     # #
# # #     # #     # #
# # -     # -     # #
# # #########     # #
# - #       #     # #
# ### #-### ####### #
# ### -     #       #
# ### ##-## # ##### #
# ### #   # #    ## #
# ### #   # ####-## #
#     #   #         #
#####################
", "\r\n" + StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void RoomMazeGenerator_GenerateWithoutRooms_SameAsTreeMazeGenerator()
        {
            var settings = new Settings
            {
                Width = 21,
                Height = 21,
                Random = new Random(0),
                AdditionalPassagesTries = 0,
                RemoveDeadEnds = false,
                NumRoomTries = 0,
                WindingPercent = 100
            };

            var result = this.Generate(settings);

            Assert.AreEqual(@"
#####################
# #     #         # #
# # ### # ### ### # #
# #   # #   # # #   #
# ### # # ### # ### #
#   # # # #   #   # #
### # # ### ##### # #
# # # #     #     # #
# # # ####### ##### #
# #   #     #   #   #
# ##### ### # # # ###
#       # # # # # # #
# ### ### # # # # # #
# #   #   # # #   # #
# # ### ### # ##### #
# #   #     # #     #
# ### ####### ##### #
# #   #     # #     #
# # ### ### # # ### #
# #       #     #   #
#####################
", "\r\n" + StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void RoomMazeGenerator_GenerateRoomsWithSizeLimit_AllRoomsShouldFitCriteria()
        {
            var settings = new Settings
            {
                Width = 201,
                Height = 201,
                MinRoomSize = 10,
                MaxRoomSize = 100,
                MaxWidthHeightRoomSizeDifference = 20
            };

            var result = this.Generate(settings);

            foreach (var r in result.Rooms)
            {
                Assert.LessOrEqual(r.Width, 100);
                Assert.LessOrEqual(r.Height, 100);
                Assert.GreaterOrEqual(r.Width, 10);
                Assert.GreaterOrEqual(r.Height, 10);
                Assert.LessOrEqual(Math.Abs(r.Height - r.Width), 20);
            }
        }

        [Test]
        public void RoomMazeGenerator_RoomSizeValuesInvalid_ThrowsException()
        {
            var settings = new Settings
            {
                Width = 201,
                Height = 201,
                MinRoomSize = 1000,
                MaxRoomSize = 100,
                MaxWidthHeightRoomSizeDifference = 20
            };

            Assert.Throws<Exception>(() => { this.Generate(settings); });
        }

        [Test]
        public void RoomMazeGenerator_GenerateRoomsWithEqualSizeValues_AllRoomsHaveEqualSizes()
        {
            var settings = new Settings
            {
                Width = 201,
                Height = 201,
                MinRoomSize = 11,
                MaxRoomSize = 11,
                MaxWidthHeightRoomSizeDifference = 20
            };

            var result = this.Generate(settings);
            foreach (var r in result.Rooms)
            {
                Assert.AreEqual(r.Width, 11);
                Assert.AreEqual(r.Height, 11);
            }
        }

        [Test]
        public void RoomMazeGenerator_TooManyAdditionalPassages_NotAllJunctionsGenerated()
        {
            var settings = new Settings
            {
                Width = 11,
                Height = 11,
                NumRoomTries = 5,
                MinRoomSize = 3,
                MaxRoomSize = 3,
                AdditionalPassagesTries = 100,
            };
            
            var result = this.Generate(settings);
            
            Assert.LessOrEqual(result.Junctions.Count, 105);
        }



        [Test]
        public void TreeMazeGenerator_GenerateWithoutRoomsAndDeadEnds_EmptyMaze()
        {
            var settings = new Settings
            {
                Width = 21,
                Height = 21,
                Random = new Random(0),
                AdditionalPassagesTries = 0,
                RemoveDeadEnds = true,
                NumRoomTries = 0,
            };
            
            var result = this.Generate(settings);

            Assert.AreEqual(@"
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
########### #########
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
", "\r\n" + StringParserAlgorithm.Stringify(result, settings));
        }
    }
}
