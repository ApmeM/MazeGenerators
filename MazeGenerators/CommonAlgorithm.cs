using MazeGenerators.Utils;
using System;

namespace MazeGenerators
{
    public class CommonAlgorithm
    {
        public static void GenerateField(GeneratorResult result, GeneratorSettings settings)
        {
            if (settings.Width % 2 == 0 || settings.Height % 2 == 0)
            {
                throw new Exception("The map must be odd-sized.");
            }

            result.Paths = new int?[settings.Width, settings.Height];
        }

        public static int? GetTile(GeneratorResult result, Vector2 pos)
        {
            return result.Paths[pos.X, pos.Y];
        }

        public static void SetTile(GeneratorResult result, Vector2 pos, int regionId)
        {
            result.Paths[pos.X, pos.Y] = regionId;
        }

        public static void RemoveTile(GeneratorResult result, Vector2 pos)
        {
            result.Paths[pos.X, pos.Y] = null;
        }

        public static bool IsInRegion(GeneratorResult result, Vector2 loc)
        {
            return loc.X >= 0 && loc.Y >= 0 && loc.X < result.Paths.GetLength(0) && loc.Y < result.Paths.GetLength(1);
        }
    }
}
