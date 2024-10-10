using System.Collections.Generic;

namespace MazeGenerators
{
    public class Maze
    {
        private Tile[,] Paths { get; set; }
        public int Width => this.Paths.GetLength(0);
        public int Height => this.Paths.GetLength(1);
        public List<Vector2> Junctions { get; } = new List<Vector2>();

        public Vector2[] Directions = new[]{
            new Vector2( 1, 0 ),
            new Vector2( 0, -1 ),
            new Vector2( -1, 0 ),
            new Vector2( 0, 1 ),
        };

        private bool roomsDirty = true;
        private Dictionary<Vector2, int> cellToRegion = new Dictionary<Vector2, int>();
        private FingerMath.Collections.Lookup<int, Vector2> regionToCells = new FingerMath.Collections.Lookup<int, Vector2>();
        public Dictionary<Vector2, int> CellToRegion
        {
            get
            {
                if (!roomsDirty)
                {
                    return cellToRegion;
                }
                RefreshRegions();
                return cellToRegion;
            }
        }

        public FingerMath.Collections.Lookup<int, Vector2> RegionToCells
        {
            get
            {
                if (!roomsDirty)
                {
                    return regionToCells;
                }
                RefreshRegions();
                return regionToCells;
            }
        }

        private void RefreshRegions()
        {
            this.regionToCells.Clear();
            this.cellToRegion.Clear();

            // Find all unconnected regions and assign numbers to them.
            int regionId = 0;
            for (var x = 0; x < this.Width; x++)
                for (var y = 0; y < this.Height; y++)
                {
                    var colored = FloodFill(new Vector2(x, y), regionId);
                    if (colored)
                    {
                        regionId++;
                    }
                }
        }

        private bool FloodFill(Vector2 pos, int color)
        {
            if (this.GetTile(new Vector2(pos.X, pos.Y)) != Tile.MazeTileId)
            {
                return false;
            }

            if (this.cellToRegion.ContainsKey(pos))
            {
                return false;
            }

            regionToCells.Add(color, pos);
            cellToRegion.Add(pos, color);

            foreach (var dir in this.Directions)
            {
                FloodFill(pos + dir, color);
            }

            return true;
        }

        public Maze(int width, int height)
        {
            this.Paths = new Tile[width, height];
            this.roomsDirty = true;
        }

        public Tile GetTile(Vector2 pos)
        {
            if (!IsInRegion(pos))
            {
                return Tile.WallTileId;
            }
            return Paths[pos.X, pos.Y];
        }

        public Maze SetTile(Vector2 pos, Tile tileId)
        {
            if (!IsInRegion(pos))
            {
                return this;
            }

            this.Paths[pos.X, pos.Y] = tileId;
            this.roomsDirty = true;
            return this;
        }

        public bool IsInRegion(Vector2 loc)
        {
            return loc.X >= 0 && loc.Y >= 0 && loc.X < Width && loc.Y < Height;
        }

        public void Init(int newWidth, int newHeight)
        {
            this.Paths = new Tile[newWidth, newHeight];
            this.roomsDirty = true;
        }

        public Tile[,] GetPathsClone()
        {
            return (Tile[,])this.Paths.Clone();
        }

        public void SetPaths(Tile[,] newPath)
        {
            this.Paths = newPath;
            this.roomsDirty = true;
        }
    }
}
