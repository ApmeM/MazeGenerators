namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class MazeTest
    {
        [Test]
        public void DrawFullRect_OutOfRange_Exception()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                new Maze(7, 7).DrawFullRect(new Rectangle(4, 4, 8, 8), Tile.MazeTileId);
            });
        }

        [Test]
        public void DrawFullRect_ValidValues_PathAdded()
        {
            var result = new Maze(7, 7).DrawFullRect(new Rectangle(2, 2, 3, 4), Tile.MazeTileId);

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
        public void DrawRect_ValidValues_PathAdded()
        {
            var result = new Maze(7, 7).DrawRect(new Rectangle(2, 2, 3, 4), Tile.MazeTileId);

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
        public void SetTile_ValidValues_PathAdded()
        {
            var result = new Maze(7,7).SetTile(new Vector2(2, 2), Tile.MazeTileId);

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
