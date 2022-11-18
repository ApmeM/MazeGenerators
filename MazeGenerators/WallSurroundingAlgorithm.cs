using MazeGenerators.Utils;

namespace MazeGenerators
{
    public class WallSurroundingAlgorithm
    {
        public static void BuildWalls(GeneratorResult result, GeneratorSettings settings)
        {
            // Fill in all of the empty space with mazes.
            for (var x = 1; x < settings.Width - 1; x++)
            {
                for (var y = 1; y < settings.Height - 1; y++)
                {
                    var pos = new Vector2(x, y);
                    if (result.GetTile(pos) == settings.WallTileId ||
                        result.GetTile(pos) == settings.EmptyTileId)
                        continue;

                    foreach (var dir in DefaultDirections.CompassDirs)
                    {
                        if (result.GetTile(pos + dir) == settings.EmptyTileId)
                        {
                            result.SetTile(pos + dir, settings.WallTileId);
                        }
                    }
                }
            }
        }
    }
}
