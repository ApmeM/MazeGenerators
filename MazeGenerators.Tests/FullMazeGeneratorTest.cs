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
        [Test]
        public void RoomMazeGenerator_Generate_SomeMaze()
        {
            var settings = new GeneratorSettings
            {
                Width = 21,
                Height = 21,
                Random = new Random(0),
            };

            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings, 10, 4, true, 2, 5, 5);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings, 0);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings, 10);
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            WallSurroundingAlgorithm.BuildWalls(result, settings);

            Assert.AreEqual(@"
#####################
#.-...........#.....#
#.#.###-###.-.#.....#
#.#.# #...#.#.#.....#
#.#.#####.#.-.#.....#
#.#.......#.#.-.....#
#.#.#######.#########
#.#.#.....#.........#
#.#.#.....#.#######.#
#.#.#.....#.#.....#.#
#.#.#.....#.#.....-.#
#.#.-.....-.-.....#.#
#.#.#####-###.....#.#
#.#.#.......#.....#.#
#.#.#.#####.#######.#
#.#.-.##....#.......#
#.-.#.##-##.#.#####.#
#.###.#...#.-.#   #.#
#.###.#...#.#######.#
#.....#...#.........#
#####################
".Replace("\r\n", "\n"), "\n" + StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void RoomMazeGenerator_GenerateWithoutRooms_SameAsTreeMazeGenerator()
        {
            var settings = new GeneratorSettings
            {
                Width = 21,
                Height = 21,
                Random = new Random(0),
            };

            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings, 0, 4, true, 2, 5, 5);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings, 100);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings, 0);
            WallSurroundingAlgorithm.BuildWalls(result, settings);

            Assert.AreEqual(@"
#####################
#.#.....#.........#.#
#.#.###.#.###.###.#.#
#.#...#.#...#.#.#...#
#.###.#.#.###.#.###.#
#...#.#.#.#...#...#.#
###.#.#.###.#####.#.#
#.#.#.#.....#.....#.#
#.#.#.#######.#####.#
#.#...#.....#...#...#
#.#####.###.#.#.#.###
#.......#.#.#.#.#.#.#
#.###.###.#.#.#.#.#.#
#.#...#...#.#.#...#.#
#.#.###.###.#.#####.#
#.#...#.....#.#.....#
#.###.#######.#####.#
#.#...#.....#.#.....#
#.#.###.###.#.#.###.#
#.#.......#.....#...#
#####################
".Replace("\r\n", "\n"), "\n" + StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void RoomMazeGenerator_TooManyAdditionalPassages_NotAllJunctionsGenerated()
        {
            var settings = new GeneratorSettings
            {
                Width = 11,
                Height = 11,
            };

            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings, 5, 4, true, 2, 3, 3);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings, 0);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings, 100);
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            WallSurroundingAlgorithm.BuildWalls(result, settings);

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
            };
            
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            RoomGeneratorAlgorithm.GenerateRooms(result, settings, 0, 4, true, 2, 5, 5);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings, 0);
            RegionConnectorAlgorithm.GenerateConnectors(result, settings, 0);
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            WallSurroundingAlgorithm.BuildWalls(result, settings);


            Assert.AreEqual(@"
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
".Replace("\r\n", "\n"), "\n" + StringParserAlgorithm.Stringify(result, settings));
        }
    }
}
