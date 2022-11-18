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
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings, 
"     \n" +
" . . \n" +
"  .. \n" +
" .   \n" +
"     \n");
            WallSurroundingAlgorithm.BuildWalls(result, settings);
            Assert.AreEqual(
"#####\n" +
"#.#.#\n" +
"##..#\n" +
"#.###\n" +
"###  \n", StringParserAlgorithm.Stringify(result, settings));
            Assert.AreEqual(0, result.Junctions.Count);
        }
    }
}
