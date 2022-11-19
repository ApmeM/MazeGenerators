using System;
using MazeGenerators.Utils;

namespace MazeGenerators
{
    public class LifeGameAlgorithm
    {
        public static Func<int, bool> DefaultBirthCondition = (n) => n == 3;
        public static Func<int, bool> DefaultDeathCondition = (n) => n < 3 || n > 4;

        public static void Life(GeneratorResult result, GeneratorSettings settings, int iterations, int liveTileId, int emptyTileId, Func<int, bool> birthCondition = null, Func<int, bool> deathCondition = null)
        {
            birthCondition = birthCondition ?? DefaultBirthCondition;
            deathCondition = deathCondition ?? DefaultDeathCondition;

            var prevGen = (int[,])result.Paths.Clone();
            var newGen = (int[,])result.Paths.Clone();

            for (var i = 0; i < iterations; i++)
            {
                var tmp = newGen;
                newGen = prevGen;
                prevGen = tmp;

                for (var x = 0; x < settings.Width; x++)
                    for (var y = 0; y < settings.Height; y++)
                    {
                        var numberOfNeighbours = 0;
                        for (var x1 = 0; x1 < 3; x1++)
                            for (var y1 = 0; y1 < 3; y1++)
                            {
                                var pos = new Vector2(x + x1 - 1, y + y1 - 1);
                                if (!result.IsInRegion(pos))
                                {
                                    continue;
                                }

                                if (prevGen[pos.X, pos.Y] == liveTileId)
                                {
                                    numberOfNeighbours++;
                                }
                            }

                        if (prevGen[x, y] == emptyTileId)
                        {
                            newGen[x, y] = birthCondition(numberOfNeighbours) ? liveTileId : emptyTileId;
                        }
                        else
                        {
                            newGen[x, y] = deathCondition(numberOfNeighbours) ? emptyTileId : liveTileId;
                        }
                    }
            }
            result.Paths = newGen;
        }
    }
}
