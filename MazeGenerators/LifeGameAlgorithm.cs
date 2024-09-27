using System;

namespace MazeGenerators
{
    public static class LifeGameAlgorithm
    {
        public static Func<int, bool> DefaultBirthCondition = (n) => n >= 6;
        public static Func<int, bool> DefaultDeathCondition = (n) => n <= 3;

        public static Maze Life(this Maze result, int iterations = 1, Tile liveTileId = Tile.MazeTileId, Tile emptyTileId = Tile.EmptyTileId, Func<int, bool> birthCondition = null, Func<int, bool> deathCondition = null)
        {
            birthCondition = birthCondition ?? DefaultBirthCondition;
            deathCondition = deathCondition ?? DefaultDeathCondition;

            var prevGen = (Tile[,])result.Paths.Clone();
            var newGen = (Tile[,])result.Paths.Clone();

            for (var i = 0; i < iterations; i++)
            {
                var tmp = newGen;
                newGen = prevGen;
                prevGen = tmp;

                for (var x = 0; x < result.Width; x++)
                    for (var y = 0; y < result.Height; y++)
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
            return result;
        }
    }
}
