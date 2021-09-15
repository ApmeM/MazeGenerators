namespace MazeGenerators.Tests
{
    using System;
    using System.Diagnostics;
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class FullMazeGeneratorTest
    {
        public GeneratorResult Generate(GeneratorSettings settings, bool needDeadendRemover = true)
        {
            var result = new GeneratorResult();
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
            if (needDeadendRemover)
            {
                sw.Reset();
                sw.Start();
                DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
                sw.Stop();
                Console.WriteLine($"RemoveDeadEnds : {sw.ElapsedMilliseconds}");
            }
            return result;
        }

        [Test]
        public void RoomMazeGenerator_Generate_SomeMaze()
        {
            var settings = new GeneratorSettings
            {
                Width = 21,
                Height = 21,
                Random = new Random(0),
                NumRoomTries = 10,
                WallTileId = 33
            };

            var result = this.Generate(settings);

            Assert.AreEqual(@"
#####################
# -           #     #
# # ###-### - #     #
# # ###   # # #     #
# # ##### # - #     #
# #       # # -     #
# # ####### #########
# # #     #         #
# # #     # ####### #
# # #     # #     # #
# # #     # #     - #
# # -     - -     # #
# # #####-###     # #
# # #       #     # #
# # # ##### ####### #
# # - ##    #       #
# - # ##-## # ##### #
# ### #   # - ##### #
# ### #   # ####### #
#     #   #         #
#####################
", "\r\n" + StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void RoomMazeGenerator_GenerateWithoutRooms_SameAsTreeMazeGenerator()
        {
            var settings = new GeneratorSettings
            {
                Width = 21,
                Height = 21,
                Random = new Random(0),
                AdditionalPassagesTries = 0,
                NumRoomTries = 0,
                WindingPercent = 100
            };

            var result = this.Generate(settings, false);

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
        public void RoomMazeGenerator_TooManyAdditionalPassages_NotAllJunctionsGenerated()
        {
            var settings = new GeneratorSettings
            {
                Width = 11,
                Height = 11,
                NumRoomTries = 5,
                MinRoomSize = 3,
                MaxRoomSize = 3,
                AdditionalPassagesTries = 100,
            };

            var result = this.Generate(settings);

            Assert.LessOrEqual(result.Junctions.Count, 20);
        }

        [Test]
        public void TreeMazeGenerator_GenerateWithoutRoomsAndDeadEnds_EmptyMaze()
        {
            var settings = new GeneratorSettings
            {
                Width = 21,
                Height = 21,
                Random = new Random(0),
                AdditionalPassagesTries = 0,
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
#####################
", "\r\n" + StringParserAlgorithm.Stringify(result, settings));
        }
    }
}
