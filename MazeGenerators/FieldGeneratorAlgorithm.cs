using MazeGenerators.Utils;
using System;

namespace MazeGenerators
{
    public class FieldGeneratorAlgorithm
    {
        public static void GenerateField(GeneratorResult result, GeneratorSettings settings)
        {
            if (settings.Width % 2 == 0 || settings.Height % 2 == 0)
            {
                throw new Exception("The map must be odd-sized.");
            }

            result.Paths = new int[settings.Width, settings.Height];
            for (var x = 0; x < settings.Width; x++)
                for (var y = 0; y < settings.Height; y++)
                {
                    result.SetTile(new Vector2(x, y), settings.EmptyTileId);
                }
        }
    }
}
