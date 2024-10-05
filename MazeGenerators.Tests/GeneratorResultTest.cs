namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class GeneratorResultTest
    {
        [Test]
        public void GenerateField_ValidValues_ArrayCreated()
        {
            var result = new Maze(3,5);
            Assert.AreEqual(3, result.Width);
            Assert.AreEqual(5, result.Height);
        }

        [Test]
        public void GenerateField_NegativeWidth_ExceptionThrown()
        {
            Assert.Throws<OverflowException>(() => { new Maze(-3, 5); });
        }

        [Test]
        public void GenerateField_NegativeHeight_ExceptionThrown()
        {
            Assert.Throws<OverflowException>(() => { new Maze(3, -5); });
        }

        [Test]
        public void GetTile_NothingSet_Null()
        {
            var result = new Maze(3, 5);
            var tile = result.GetTile(new Vector2(1, 1));
            Assert.AreEqual(tile, Tile.EmptyTileId);
        }

        [Test]
        public void GetTile_OutsideArray_ExceptionThrown()
        {
            var result = new Maze(3,5);
            Assert.Throws<IndexOutOfRangeException>(() => { result.GetTile(new Vector2(10, 10)); });
        }

        [Test]
        public void SetTile_ValueSet_ReturnedInGetTile()
        {
            var result = new Maze(3,5);
            result.SetTile(new Vector2(1, 1), Tile.MazeTileId);

            var tile = result.GetTile(new Vector2(1, 1));

            Assert.AreEqual(Tile.MazeTileId, tile);
        }
    }
}
