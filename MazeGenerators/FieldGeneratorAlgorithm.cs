using MazeGenerators.Utils;

namespace MazeGenerators
{
    public class FieldGeneratorAlgorithm
    {
        public static void GenerateField(GeneratorResult result, GeneratorSettings settings)
        {
            result.Paths = new int[settings.Width, settings.Height];
            for (var x = 0; x < settings.Width; x++)
                for (var y = 0; y < settings.Height; y++)
                {
                    result.SetTile(new Vector2(x, y), settings.EmptyTileId);
                }
        }
    }
}
