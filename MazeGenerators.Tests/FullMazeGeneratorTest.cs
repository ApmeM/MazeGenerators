namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class FluentTest
    {
        [Test]
        public void RoomMazeGenerator_Generate_SomeMaze()
        {
            var r = new Random(0);
            var result = new Maze(21, 21)
                .GenerateRooms((max) => r.Next(max), 10, 4, true, 2, 5, 5)
                .GrowMaze((max) => r.Next(max), DefaultDirections.CardinalDirs, 0)
                .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs, 10)
                .RemoveDeadEnds(DefaultDirections.CardinalDirs)
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
        public void RoomMazeGenerator_CustomPaint_SomeMaze()
        {
            var result = new Maze(13, 9)
                .AddFillRectangle(new Rectangle(1, 1, 3, 4), Tile.MazeTileId)
                .AddRectangle(new Rectangle(5, 1, 7, 7), Tile.MazeTileId)
                .SetTile(new Vector2(7, 3), Tile.MazeTileId)
                .BuildWalls()
                .Stringify();

            Assert.AreEqual(@"
#############
#...#.......#
#...#.#####.#
#...#.#.# #.#
#...#.### #.#
#####.#   #.#
    #.#####.#
    #.......#
    #########
".Replace("\r\n", "\n"), "\n" + result);
        }

        [Test]
        public void RoomMazeGenerator_GenerateWithoutRooms_SameAsTreeMazeGenerator()
        {
            var r = new Random(0);
            var result = new Maze(21, 21)
                .GenerateRooms((max) => r.Next(max), 0, 4, true, 2, 5, 5)
                .GrowMaze((max) => r.Next(max), DefaultDirections.CardinalDirs, 100)
                .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs, 0)
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
            var r = new Random(0);
            var result = new Maze(11, 11)
                .GenerateRooms((max) => r.Next(max), 5, 4, true, 2, 3, 3)
                .GrowMaze((max) => r.Next(max), DefaultDirections.CardinalDirs,0)
                .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs,100)
                .RemoveDeadEnds(DefaultDirections.CardinalDirs)
                .BuildWalls();

            Assert.LessOrEqual(result.Junctions.Count, 20);
        }

        [Test]
        public void TreeMazeGenerator_GenerateWithoutRoomsAndDeadEnds_EmptyMaze()
        {
            var r = new Random(0);

            var result = new Maze(21, 21)
                .GenerateRooms((max) => r.Next(max), 0, 4, true, 2, 5, 5)
                .GrowMaze((max) => r.Next(max), DefaultDirections.CardinalDirs, 0)
                .GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs, 0)
                .RemoveDeadEnds(DefaultDirections.CardinalDirs)
                .BuildWalls()
                .Stringify();

            Assert.AreEqual(@"
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
                     
".Replace("\r\n", "\n"), "\n" + result);
        }
    }
}
