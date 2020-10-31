namespace MazeGenerators
{
    using System;
    using System.Collections.Generic;

    using MazeGenerators.Utils;

    public class TreeMazeGenerator
    {
        public class Result
        {
            public int?[,] Regions;

            public int? GetTile(Vector2 pos)
            {
                return this.Regions[pos.X, pos.Y];
            }

            public void SetTile(Vector2 pos, int regionId)
            {
                this.Regions[pos.X, pos.Y] = regionId;
            }

            public void RemoveTile(Vector2 pos)
            {
                this.Regions[pos.X, pos.Y] = null;
            }

            public bool IsInRegion(Vector2 loc)
            {
                return loc.X >= 0 && loc.Y >= 0 && loc.X < this.Regions.GetLength(0) && loc.Y < this.Regions.GetLength(1);
            }
        }

        public class Settings
        {
            public Settings(int width, int height)
            {
                this.Width = width;
                this.Height = height;
            }

            public int Width;

            public int Height;

            public Random Random = new Random();

            public Vector2[] Directions = MazeGenerators.Directions.CardinalDirs;

            public int WindingPercent = 100;
        }

        public Result Generate(Settings settings)
        {
            if (settings.Width % 2 == 0 || settings.Height % 2 == 0)
            {
                throw new Exception("The stage must be odd-sized.");
            }

            var result = new Result
            {
                Regions = new int?[settings.Width, settings.Height]
            };

            this.GrowMaze(result, settings, new Vector2(1, 1), 1);

            return result;
        }

        private void GrowMaze(Result result, Settings settings, Vector2 start, int regionId)
        {
            var cells = new List<Vector2>();

            result.SetTile(start, regionId);

            cells.Add(start);

            Vector2? lastDir = null;

            while (cells.Count > 0)
            {
                var cell = cells[cells.Count - 1];

                var unmadeCells = new List<Vector2>();

                foreach (var dir in settings.Directions)
                {
                    if (this.CanCarve(result, cell, dir))
                        unmadeCells.Add(dir);
                }

                if (unmadeCells.Count != 0)
                {
                    Vector2 dir;
                    if (lastDir != null && unmadeCells.Contains(lastDir.Value) && settings.Random.Next(100) > settings.WindingPercent)
                    {
                        dir = lastDir.Value;
                    }
                    else
                    {
                        dir = unmadeCells[settings.Random.Next(unmadeCells.Count)];
                    }

                    result.SetTile(cell + dir, regionId);
                    result.SetTile(cell + dir * 2, regionId);

                    cells.Add(cell + dir * 2);
                    lastDir = dir;
                }
                else
                {
                    cells.RemoveAt(cells.Count - 1);
                    lastDir = null;
                }
            }
        }

        private bool CanCarve(Result result, Vector2 pos, Vector2 direction)
        {
            // Must end in bounds.
            var block = pos + direction * 3;
            if (!result.IsInRegion(block))
                return false;

            // Destination must not be open.
            var end = pos + direction * 2;
            return !result.GetTile(end).HasValue;
        }
    }
}