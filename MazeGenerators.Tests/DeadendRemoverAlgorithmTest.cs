namespace MazeGenerators.Tests
{
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class DeadendRemoverAlgorithmTest
    {
        [Test]
        public void RemoveDeadEnds_LinearMaze_AllRemoved()
        {
            var settings = new GeneratorSettings
            {
                Width = 3,
                Height = 5,
            };
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings, 
"   \n" +
" . \n" +
" . \n" +
" . \n" +
"   \n");
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            Assert.AreEqual(
"   \n" +
"   \n" +
"   \n" +
"   \n" +
"   \n", StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void RemoveDeadEnds_NoDeadEnd_NothingChanged()
        {
            var settings = new GeneratorSettings
            {
                Width = 5,
                Height = 5,
            };
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings, 
"     \n" +
" ... \n" +
" . . \n" +
" ... \n" +
"     \n");
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            Assert.AreEqual(
"     \n" +
" ... \n" +
" . . \n" +
" ... \n" +
"     \n", StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void RemoveDeadEnds_MixedMaze_OnlyDeadendsRemoved()
        {
            var settings = new GeneratorSettings
            {
                Width = 5,
                Height = 5,
            };
            var result = new GeneratorResult();
            CommonAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings, 
"     \n" +
" ... \n" +
" ..  \n" +
" ... \n" +
"     \n");
            DeadEndRemoverAlgorithm.RemoveDeadEnds(result, settings);
            Assert.AreEqual(
"     \n" +
" ..  \n" +
" ..  \n" +
" ..  \n" +
"     \n", StringParserAlgorithm.Stringify(result, settings));
        }
    }
}
