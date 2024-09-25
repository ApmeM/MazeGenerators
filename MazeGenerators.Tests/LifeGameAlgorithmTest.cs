namespace MazeGenerators.Tests
{
    using NUnit.Framework;
    using MazeGenerators;
    using System;

    [TestFixture]
    public class LifeGameAlgorithmTest
    {
        [Test]
        public void Life_SingleIterationForPlaner_PlanerMoved()
        {
            var result = new Maze(7, 7).Parse(
"       \n" +
"  .    \n" +
". .    \n" +
" ..    \n" +
"       \n" +
"       \n" +
"       \n");
            
            result.Life(1, Tile.MazeTileId, Tile.EmptyTileId, (n) => n == 3, (n) => n < 3 || n > 4);

            Assert.AreEqual(
"       \n" +
" .     \n" +
"  ..   \n" +
" ..    \n" +
"       \n" +
"       \n" +
"       \n", result.Stringify());
        }

        [Test]
        public void Life_MultipleIterationForPlaner_PlanerMoved()
        {
            var result = new Maze(7, 7).Parse(
"       \n" +
"  .    \n" +
". .    \n" +
" ..    \n" +
"       \n" +
"       \n" +
"       \n");
            
            result.Life(4, Tile.MazeTileId, Tile.EmptyTileId, (n) => n == 3, (n) => n < 3 || n > 4);

            Assert.AreEqual(
"       \n" +
"       \n" +
"   .   \n" +
" . .   \n" +
"  ..   \n" +
"       \n" +
"       \n", result.Stringify());
        }
    }
}
