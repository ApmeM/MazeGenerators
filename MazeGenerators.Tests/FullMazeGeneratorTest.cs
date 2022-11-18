namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class FluentTest
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

            var result = Fluent
                .Build(settings)
                .GenerateField()
                .GenerateRooms(10, 4, true, 2, 5, 5)
                .GrowMaze(0)
                .GenerateConnectors(10)
                .RemoveDeadEnds()
                .BuildWalls()
                .Stringify();

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
".Replace("\r\n", "\n"), "\n" + result);
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


            var result = Fluent
                .Build(settings)
                .GenerateField()
                .GenerateRooms(0, 4, true, 2, 5, 5)
                .GrowMaze(100)
                .GenerateConnectors(0)
                .BuildWalls()
                .Stringify();

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
".Replace("\r\n", "\n"), "\n" + result);
        }

        [Test]
        public void RoomMazeGenerator_TooManyAdditionalPassages_NotAllJunctionsGenerated()
        {
            var settings = new GeneratorSettings
            {
                Width = 11,
                Height = 11,
            };

            var result = Fluent
                .Build(settings)
                .GenerateField()
                .GenerateRooms(5, 4, true, 2, 3, 3)
                .GrowMaze(0)
                .GenerateConnectors(100)
                .RemoveDeadEnds()
                .BuildWalls();

            Assert.LessOrEqual(result.result.Junctions.Count, 20);
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

            var result = Fluent
                .Build(settings)
                .GenerateField()
                .GenerateRooms(0, 4, true, 2, 5, 5)
                .GrowMaze(0)
                .GenerateConnectors(0)
                .RemoveDeadEnds()
                .BuildWalls()
                .Stringify();

            Assert.AreEqual(@"
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
".Replace("\r\n", "\n"), "\n" + result);
        }
    }
}
