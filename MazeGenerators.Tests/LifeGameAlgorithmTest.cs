namespace MazeGenerators.Tests
{
    using MazeGenerators.Utils;
    using NUnit.Framework;
    using MazeGenerators;
    using System;

    [TestFixture]
    public class LifeGameAlgorithmTest
    {
        [Test]
        public void Life_SingleIterationForPlaner_PlanerMoved()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
                Random = new Random(0),
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings,
"       \n" +
"  .    \n" +
". .    \n" +
" ..    \n" +
"       \n" +
"       \n" +
"       \n");
            
            LifeGameAlgorithm.Life(result, settings, 1, settings.MazeTileId, settings.EmptyTileId, (n) => n == 3, (n) => n < 3 || n > 4);

            Assert.AreEqual(
"       \n" +
" .     \n" +
"  ..   \n" +
" ..    \n" +
"       \n" +
"       \n" +
"       \n", StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void Life_MultipleIterationForPlaner_PlanerMoved()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
                Random = new Random(0),
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            StringParserAlgorithm.Parse(result, settings,
"       \n" +
"  .    \n" +
". .    \n" +
" ..    \n" +
"       \n" +
"       \n" +
"       \n");
            
            LifeGameAlgorithm.Life(result, settings, 4, settings.MazeTileId, settings.EmptyTileId, (n) => n == 3, (n) => n < 3 || n > 4);

            Assert.AreEqual(
"       \n" +
"       \n" +
"   .   \n" +
" . .   \n" +
"  ..   \n" +
"       \n" +
"       \n", StringParserAlgorithm.Stringify(result, settings));
        }
    }
}
