using MazeGenerators.Common;
using MazeGenerators.Utils;

namespace MazeGenerators.DeadEndRemover
{
    public class DeadEndRemoverAlgorithm
    {
        public static void RemoveDeadEnds(IDeadEndRemoverResult result, IDeadEndRemoverSettings settings)
        {
            for (var x = 0; x < settings.Width; x++)
                for (var y = 0; y < settings.Height; y++)
                {
                    var pos = new Vector2(x, y);
                    if (!CommonAlgorithm.GetTile(result, pos).HasValue)
                    {
                        continue;
                    }

                    // If it only has one exit, it's a dead end.
                    int exits;
                    do
                    {
                        exits = 0;
                        Vector2 lastExitPosition = new Vector2(0, 0);
                        foreach (var dir in settings.Directions)
                        {
                            if (!CommonAlgorithm.IsInRegion(result, pos + dir))
                            {
                                continue;
                            }

                            if (!CommonAlgorithm.GetTile(result, pos + dir).HasValue)
                            {
                                continue;
                            }

                            exits++;
                            lastExitPosition = pos + dir;
                        }

                        if (exits == 0)
                        {
                            CommonAlgorithm.RemoveTile(result, pos);
                        } else if (exits == 1)
                        {
                            CommonAlgorithm.RemoveTile(result, pos);
                            pos = lastExitPosition;
                        }
                    } while (exits == 1);
                }
        }
    }
}
