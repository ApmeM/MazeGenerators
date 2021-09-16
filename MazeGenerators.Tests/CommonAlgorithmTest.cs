namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class CommonAlgorithmTest
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
            CommonAlgorithm.GenerateField(result, settings);
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

            Assert.Throws<Exception>(() => { CommonAlgorithm.GenerateField(result, settings); });
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

            Assert.Throws<OverflowException>(() => { CommonAlgorithm.GenerateField(result, settings); });
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

            Assert.Throws<Exception>(() => { CommonAlgorithm.GenerateField(result, settings); });
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

            Assert.Throws<OverflowException>(() => { CommonAlgorithm.GenerateField(result, settings); });
        }

        [Test]
        public void GetTile_NothingSet_Null()
        {
            var settings = new GeneratorSettings
            {
                Width = 3,
                Height = 5,
                WallTileId = 123,
                EmptyTileId = 321
            };
            var result = new GeneratorResult();

            CommonAlgorithm.GenerateField(result, settings);

            var tile = CommonAlgorithm.GetTile(result, new Vector2(1, 1));
            
            Assert.AreEqual(tile, settings.EmptyTileId);
        }

        [Test]
        public void GetTile_OutsideArray_ExceptionThrown()
        {
            var settings = new GeneratorSettings
            {
                Width = 3,
                Height = 5
            };
            var result = new GeneratorResult();

            CommonAlgorithm.GenerateField(result, settings);

            Assert.Throws<IndexOutOfRangeException>(() => { CommonAlgorithm.GetTile(result, new Vector2(10, 10)); });
        }

        [Test]
        public void SetTile_ValueSet_ReturnedInGetTile()
        {
            var settings = new GeneratorSettings
            {
                Width = 3,
                Height = 5
            };
            var result = new GeneratorResult();

            CommonAlgorithm.GenerateField(result, settings);
            CommonAlgorithm.SetTile(result, new Vector2(1, 1), 11);

            var tile = CommonAlgorithm.GetTile(result, new Vector2(1, 1));

            Assert.AreEqual(11, tile);
        }
    }
}
