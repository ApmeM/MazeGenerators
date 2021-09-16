namespace MazeGenerators.Tests
{
    using MazeGenerators.Utils;
    using NUnit.Framework;
    using MazeGenerators;
    using System;

    [TestFixture]
    public class TreeMazeBuilderAlgorithmTest
    {
        [Test]
        public void GrowMaze_NoWinding_StrightLine()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
                Random = new Random(0),
                WindingPercent = 0
            };
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings);
            Assert.AreEqual(
"       \r\n" +
" . ... \r\n" +
" . . . \r\n" +
" . . . \r\n" +
" .   . \r\n" +
" ..... \r\n" +
"       \r\n", StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void GrowMaze_WindingAlways_TreeMaze()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
                WindingPercent = 100,
                Random = new Random(0)
            };
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            TreeMazeBuilderAlgorithm.GrowMaze(result, settings);
            Assert.AreEqual(
"       \r\n" +
" . ... \r\n" +
" .   . \r\n" +
" . ... \r\n" +
" . . . \r\n" +
" ... . \r\n" +
"       \r\n", StringParserAlgorithm.Stringify(result, settings));
        }

    }
}
