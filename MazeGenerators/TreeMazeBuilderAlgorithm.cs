﻿using System;
using System.Collections.Generic;

namespace MazeGenerators
{
    public static class TreeMazeBuilderAlgorithm
    {
        private static readonly Random r = new Random();
        private static readonly Func<int, int> defaultRandomizer = (max) => r.Next(max);

        public static Maze GrowMaze(this Maze result, Func<int, int> nextRandom = null, int windingPercent = 50)
        {
            if (result.Width % 2 == 0 || result.Height % 2 == 0)
            {
                throw new Exception("The map must be odd-sized.");
            }

            nextRandom = nextRandom ?? defaultRandomizer;

            // Fill in all of the empty space with mazes.
            for (var x = 1; x < result.Width; x += 2)
            {
                for (var y = 1; y < result.Height; y += 2)
                {
                    var pos = new Vector2(x, y);
                    if (result.GetTile(pos) == Tile.MazeTileId)
                        continue;
                    GrowSingleMazeTree(result, pos, windingPercent, nextRandom);
                }
            }

            return result;
        }

        private static void GrowSingleMazeTree(Maze result, Vector2 start, int windingPercent, Func<int, int> nextRandom)
        {
            var cells = new Stack<Vector2>();

            result.SetTile(start, Tile.MazeTileId);

            cells.Push(start);

            Vector2? lastDir = null;

            while (cells.Count > 0)
            {
                var cell = cells.Peek();

                var unmadeCells = new List<Vector2>();

                foreach (var dir in result.Directions)
                {
                    if (CanCarve(result, cell, dir))
                    {
                        unmadeCells.Add(dir);
                    }
                }

                if (unmadeCells.Count != 0)
                {
                    Vector2 dir;
                    if (lastDir != null && unmadeCells.Contains(lastDir.Value) && nextRandom(100) > windingPercent)
                    {
                        dir = lastDir.Value;
                    }
                    else
                    {
                        dir = unmadeCells[nextRandom(unmadeCells.Count)];
                    }

                    result.SetTile(cell + dir, Tile.MazeTileId);
                    result.SetTile(cell + dir * 2, Tile.MazeTileId);

                    cells.Push(cell + dir * 2);
                    lastDir = dir;
                }
                else
                {
                    cells.Pop();
                    lastDir = null;
                }
            }
        }

        private static bool CanCarve(Maze result, Vector2 pos, Vector2 direction)
        {
            // Must end in bounds.
            var block = pos + direction * 3;
            if (!result.IsInRegion(block))
                return false;

            // Destination must not be open.
            var end = pos + direction * 2;
            return result.GetTile(end) == Tile.EmptyTileId;
        }
    }
}
