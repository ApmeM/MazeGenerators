namespace MazeGenerators.Tests
{
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class ResetAlgorithmTest
    {
        [Test]
        public void Reset_Default_ResetWithNoSizeChanged()
        {
            var result = new Maze(7, 7)
                .DrawFullRect(new Rectangle(1, 1, 1, 1), Tile.WallTileId)
                .Reset();

            Assert.AreEqual(
"       \n" +
"       \n" +
"       \n" +
"       \n" +
"       \n" +
"       \n" +
"       \n", result.Stringify());
        }

        [Test]
        public void Reset_WithNewWidth_ResetWithResize()
        {
            var result = new Maze(7, 7)
                .DrawFullRect(new Rectangle(1, 1, 1, 1), Tile.WallTileId)
                .Reset(5, -1);

            Assert.AreEqual(
"     \n" +
"     \n" +
"     \n" +
"     \n" +
"     \n" +
"     \n" +
"     \n", result.Stringify());
        }

        [Test]
        public void Reset_WithNewHeight_ResetWithResize()
        {
            var result = new Maze(7, 7)
                .DrawFullRect(new Rectangle(1, 1, 1, 1), Tile.WallTileId)
                .Reset(-1, 5);

            Assert.AreEqual(
"       \n" +
"       \n" +
"       \n" +
"       \n" +
"       \n", result.Stringify());
        }

        [Test]
        public void Reset_WithNewSizes_ResetWithResize()
        {
            var result = new Maze(7, 7)
                .DrawFullRect(new Rectangle(1, 1, 1, 1), Tile.WallTileId)
                .Reset(5, 5);

            Assert.AreEqual(
"     \n" +
"     \n" +
"     \n" +
"     \n" +
"     \n", result.Stringify());
        }
    }
}
