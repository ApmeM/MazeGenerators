namespace MazeGenerators.Tests
{
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class WallSurroundingAlgorithmTest
    {
        [Test]
        public void GenerateConnectors_SingleArea_NothingChanged()
        {
            var result = new Maze(5, 5).Parse(
"     \n" +
" . . \n" +
"  .. \n" +
" .   \n" +
"     \n");
            result.BuildWalls();
            Assert.AreEqual(
"#####\n" +
"#.#.#\n" +
"##..#\n" +
"#.###\n" +
"###  \n", result.Stringify());
            Assert.AreEqual(0, result.Junctions.Count);
        }
    }
}
