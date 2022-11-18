namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class FieldGeneratorAlgorithmTest
    {
        [Test]
        public void GenerateField_ValidValues_ArrayCreated()
        {
            var settings = new GeneratorSettings
            {
                Width = 3,
                Height = 5
            };
            var result = new GeneratorResult();
            FieldGeneratorAlgorithm.GenerateField(result, settings);
            Assert.AreEqual(3, result.Paths.GetLength(0));
            Assert.AreEqual(5, result.Paths.GetLength(1));
        }

        [Test]
        public void GenerateField_InvalidWidth_ExceptionThrown()
        {
            var settings = new GeneratorSettings
            {
                Width = 2,
                Height = 5
            };
            var result = new GeneratorResult();

            Assert.Throws<Exception>(() => { FieldGeneratorAlgorithm.GenerateField(result, settings); });
        }

        [Test]
        public void GenerateField_NegativeWidth_ExceptionThrown()
        {
            var settings = new GeneratorSettings
            {
                Width = -3,
                Height = 5
            };
            var result = new GeneratorResult();

            Assert.Throws<OverflowException>(() => { FieldGeneratorAlgorithm.GenerateField(result, settings); });
        }


        [Test]
        public void GenerateField_InvalidHeight_ExceptionThrown()
        {
            var settings = new GeneratorSettings
            {
                Width = 3,
                Height = 4
            };
            var result = new GeneratorResult();

            Assert.Throws<Exception>(() => { FieldGeneratorAlgorithm.GenerateField(result, settings); });
        }

        [Test]
        public void GenerateField_NegativeHeight_ExceptionThrown()
        {
            var settings = new GeneratorSettings
            {
                Width = 3,
                Height = -5
            };
            var result = new GeneratorResult();

            Assert.Throws<OverflowException>(() => { FieldGeneratorAlgorithm.GenerateField(result, settings); });
        }
    }
}
