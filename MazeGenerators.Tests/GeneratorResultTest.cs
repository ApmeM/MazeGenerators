namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using MazeGenerators.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class GeneratorResultTest
    {
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

            FieldGeneratorAlgorithm.GenerateField(result, settings);

            var tile = result.GetTile(new Vector2(1, 1));
            
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

            FieldGeneratorAlgorithm.GenerateField(result, settings);

            Assert.Throws<IndexOutOfRangeException>(() => { result.GetTile(new Vector2(10, 10)); });
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

            FieldGeneratorAlgorithm.GenerateField(result, settings);
            result.SetTile(new Vector2(1, 1), 11);

            var tile = result.GetTile(new Vector2(1, 1));

            Assert.AreEqual(11, tile);
        }
    }
}
