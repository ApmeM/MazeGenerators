using MazeGenerators.Common;
using MazeGenerators.Utils;

namespace MazeGenerators.DeadEndRemover
{
    public class DeadEndRemoverAlgorithm
    {
        public static void RemoveDeadEnds(IDeadEndRemoverResult result, IDeadEndRemoverSettings settings)
        {
            if (!settings.RemoveDeadEnds)
            {
                return;
            }

            var done = false;

            while (!done)
            {
                done = true;

                for (var x = 0; x < settings.Width; x++)
                    for (var y = 0; y < settings.Height; y++)
                    {
                        var pos = new Vector2(x, y);
                        if (!CommonAlgorithm.GetTile(result, pos).HasValue)
                        {
                            continue;
                        }

                        // If it only has one exit, it's a dead end.
                        var exits = 0;
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
                        }

                        if (exits != 1)
                        {
                            continue;
                        }

                        done = false;
                        CommonAlgorithm.RemoveTile(result, pos);
                    }
            }
        }
    }
}
