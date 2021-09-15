using MazeGenerators.Utils;

namespace MazeGenerators
{
    public class DeadEndRemoverAlgorithm
    {
        public static void RemoveDeadEnds(GeneratorResult result, GeneratorSettings settings)
        {
            for (var x = 0; x < settings.Width; x++)
                for (var y = 0; y < settings.Height; y++)
                {
                    var pos = new Vector2(x, y);
                    if (CommonAlgorithm.GetTile(result, pos) == settings.WallTileId)
                    {
                        continue;
                    }

                    // If it only has one exit, it's a dead end.
                    int exits;
                    do
                    {
                        exits = 0;
                        var lastExitPosition = new Vector2(0, 0);
                        foreach (var dir in settings.Directions)
                        {
                            if (!CommonAlgorithm.IsInRegion(result, pos + dir))
                            {
                                continue;
                            }

                            if (CommonAlgorithm.GetTile(result, pos + dir) == settings.WallTileId)
                            {
                                continue;
                            }

                            exits++;
                            lastExitPosition = pos + dir;
                        }

                        if (exits == 0)
                        {
                            CommonAlgorithm.SetTile(result, pos, settings.WallTileId);
                        }
                        else if (exits == 1)
                        {
                            CommonAlgorithm.SetTile(result, pos, settings.WallTileId);
                            pos = lastExitPosition;
                        }
                    } while (exits == 1);
                }
        }
    }
}
