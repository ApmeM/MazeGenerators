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
                    if (result.GetTile(pos) == settings.EmptyTileId)
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
                            if (!result.IsInRegion(pos + dir))
                            {
                                continue;
                            }

                            if (result.GetTile(pos + dir) == settings.EmptyTileId)
                            {
                                continue;
                            }

                            exits++;
                            lastExitPosition = pos + dir;
                        }

                        if (exits == 0)
                        {
                            result.SetTile(pos, settings.EmptyTileId);
                        }
                        else if (exits == 1)
                        {
                            result.SetTile(pos, settings.EmptyTileId);
                            pos = lastExitPosition;
                        }
                    } while (exits == 1);
                }
        }
    }
}
