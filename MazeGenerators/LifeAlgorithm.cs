using System;

namespace MazeGenerators
{
    public static class LifeAlgorithm
    {
        public static Func<int, bool> DefaultBirthCondition = (n) => n >= 6;
        public static Func<int, bool> DefaultDeathCondition = (n) => n <= 3;

        /// <summary>
        /// Change the maze according to game of life rules.
        /// By default live tile is of type maze and dead tile is of type empty.
        /// Default birth and death conditions is different from base rules of game of life to be more useful for maze generation.
        /// To make it work as a normal game of life use this command:
        ///   'result.Life((n) => n == 3, (n) => n < 3 || n > 4);'
        /// </summary>
        public static Maze Life(this Maze result, Func<int, bool> birthCondition = null, Func<int, bool> deathCondition = null)
        {
            birthCondition = birthCondition ?? DefaultBirthCondition;
            deathCondition = deathCondition ?? DefaultDeathCondition;

            var prevGen = (Tile[,])result.Paths.Clone();

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

                            if (prevGen[pos.X, pos.Y] == Tile.MazeTileId)
                            {
                                numberOfNeighbours++;
                            }
                        }
                    if (prevGen[x, y] == Tile.EmptyTileId)
                    {
                        result.Paths[x, y] = birthCondition(numberOfNeighbours) ? Tile.MazeTileId : Tile.EmptyTileId;
                    }
                    else
                    {
                        result.Paths[x, y] = deathCondition(numberOfNeighbours) ? Tile.EmptyTileId : Tile.MazeTileId;
                    }
                }
            return result;
        }
    }
}
