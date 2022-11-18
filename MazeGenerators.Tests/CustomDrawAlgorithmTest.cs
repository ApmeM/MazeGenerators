namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class CustomDrawAlgorithmTest
    {
        [Test]
        public void AddFillRectangle_OutOfRange_Exception()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };

            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                CustomDrawAlgorithm.AddFillRectangle(result, settings, new Rectangle(4, 4, 8, 8), settings.RoomTileId);
            });
        }

        [Test]
        public void AddFillRectangle_ValidValues_PathAdded()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };

            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            CustomDrawAlgorithm.AddFillRectangle(result, settings, new Rectangle(2, 2, 3, 4), settings.RoomTileId);

            Assert.AreEqual(
"       \n" +
"       \n" +
"  ...  \n" +
"  ...  \n" +
"  ...  \n" +
"  ...  \n" +
"       \n", StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void AddRectangle_ValidValues_PathAdded()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };

            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            CustomDrawAlgorithm.AddRectangle(result, settings, new Rectangle(2, 2, 3, 4), settings.RoomTileId);

            Assert.AreEqual(
"       \n" +
"       \n" +
"  ...  \n" +
"  . .  \n" +
"  . .  \n" +
"  ...  \n" +
"       \n", StringParserAlgorithm.Stringify(result, settings));
        }

        [Test]
        public void AddPoint_ValidValues_PathAdded()
        {
            var settings = new GeneratorSettings
            {
                Width = 7,
                Height = 7,
            };

            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            CustomDrawAlgorithm.AddPoint(result, settings, new Vector2(2, 2), settings.RoomTileId);

            Assert.AreEqual(
"       \n" +
"       \n" +
"  .    \n" +
"       \n" +
"       \n" +
"       \n" +
"       \n", StringParserAlgorithm.Stringify(result, settings));
        }
    }
}
