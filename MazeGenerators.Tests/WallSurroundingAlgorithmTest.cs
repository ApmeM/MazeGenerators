namespace MazeGenerators.Tests
{
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class WallSurroundingAlgorithmTest
    {
        [Test]
        public void GenerateConnectors_SingleArea_NothingChanged()
        {
            var settings = new GeneratorSettings
            {
                Width = 5,
                Height = 5,
                MazeText =
                "     \n" +
                " . . \n" +
                "  .. \n" +
                " .   \n" +
                "     \n"
            };
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings);
            WallSurroundingAlgorithm.BuildWalls(result, settings);
            Assert.AreEqual(
"#####\r\n" +
"#.#.#\r\n" +
"##..#\r\n" +
"#.###\r\n" +
"###  \r\n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(0, result.Junctions.Count);
        }
    }
}
