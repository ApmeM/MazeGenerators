namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class CustomDrawAlgorithmTest
    {
        [Test]
        public void AddFillRectangle_OutOfRange_Exception()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                new Maze(7, 7).AddFillRectangle(new Rectangle(4, 4, 8, 8), Tile.RoomTileId);
            });
        }

        [Test]
        public void AddFillRectangle_ValidValues_PathAdded()
        {
            var result = new Maze(7, 7).AddFillRectangle(new Rectangle(2, 2, 3, 4), Tile.RoomTileId);

            Assert.AreEqual(
"       \n" +
"       \n" +
"  ...  \n" +
"  ...  \n" +
"  ...  \n" +
"  ...  \n" +
"       \n", result.Stringify());
        }

        [Test]
        public void AddRectangle_ValidValues_PathAdded()
        {
            var result = new Maze(7, 7).AddRectangle(new Rectangle(2, 2, 3, 4), Tile.RoomTileId);

            Assert.AreEqual(
"       \n" +
"       \n" +
"  ...  \n" +
"  . .  \n" +
"  . .  \n" +
"  ...  \n" +
"       \n", result.Stringify());
        }

        [Test]
        public void AddPoint_ValidValues_PathAdded()
        {
            var result = new Maze(7,7).SetTile(new Vector2(2, 2), Tile.RoomTileId);

            Assert.AreEqual(
"       \n" +
"       \n" +
"  .    \n" +
"       \n" +
"       \n" +
"       \n" +
"       \n", result.Stringify());
        }
    }
}
