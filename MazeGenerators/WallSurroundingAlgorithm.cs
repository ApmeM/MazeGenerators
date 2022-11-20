using MazeGenerators.Utils;

namespace MazeGenerators
{
    public class WallSurroundingAlgorithm
    {
        public static void BuildWalls(GeneratorResult result, GeneratorSettings settings)
        {
            // Fill in all of the empty space with mazes.
            for (var x = 0; x < settings.Width; x++)
            {
                for (var y = 0; y < settings.Height; y++)
                {
                    var pos = new Vector2(x, y);
                    if (result.GetTile(pos) == settings.WallTileId ||
                        result.GetTile(pos) == settings.EmptyTileId)
                        continue;

                    foreach (var dir in DefaultDirections.CompassDirs)
                    {
                        var newPos = pos + dir;
                        if (!result.IsInRegion(newPos))
                        {
                            continue;
                        }

                        if (result.GetTile(newPos) == settings.EmptyTileId)
                        {
                            result.SetTile(newPos, settings.WallTileId);
                        }
                    }
                }
            }
        }
    }
}
