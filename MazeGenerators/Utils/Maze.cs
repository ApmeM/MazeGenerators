using System.Collections.Generic;

namespace MazeGenerators
{
    public class Maze
    {
        private Tile[,] Paths { get; set; }
        public int Width => this.Paths.GetLength(0);
        public int Height => this.Paths.GetLength(1);
        public List<Vector2> Junctions { get; } = new List<Vector2>();

        private bool roomsDirty = true;
        private Dictionary<Vector2, int> cellToRoom = new Dictionary<Vector2, int>();
        private FingerMath.Collections.Lookup<int, Vector2> roomToCells = new FingerMath.Collections.Lookup<int, Vector2>();
        public Dictionary<Vector2, int> CellToRoom
        {
            get
            {
                if (!roomsDirty)
                {
                    return cellToRoom;
                }
                RefreshRooms();
                return cellToRoom;
            }
        }

        public FingerMath.Collections.Lookup<int, Vector2> RoomToCells
        {
            get
            {
                if (!roomsDirty)
                {
                    return roomToCells;
                }
                RefreshRooms();
                return roomToCells;
            }
        }

        private void RefreshRooms()
        {
            this.roomToCells.Clear();
            this.cellToRoom.Clear();

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
            if (!this.IsInRegion(pos))
            {
                return false;
            }

            if (this.cellToRoom.ContainsKey(pos))
            {
                return false;
            }

            if (this.GetTile(new Vector2(pos.X, pos.Y)) != Tile.MazeTileId)
            {
                return false;
            }

            roomToCells.Add(color, pos);
            cellToRoom.Add(pos, color);

            foreach (var dir in DefaultDirections.CardinalDirs)
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
            return Paths[pos.X, pos.Y];
        }

        public Maze SetTile(Vector2 pos, Tile tileId)
        {
            this.Paths[pos.X, pos.Y] = tileId;
            this.roomsDirty = true;
            return this;
        }

        public Maze DrawFullRect(Rectangle rect, Tile tileId)
        {
            var result = this;
            for (var x = rect.X; x < rect.X + rect.Width; x++)
                for (var y = rect.Y; y < rect.Y + rect.Height; y++)
                {
                    result.SetTile(new Vector2(x, y), tileId);
                }
            return result;
        }

        public Maze DrawRect(Rectangle rect, Tile tileId)
        {
            var result = this;
            for (var x = rect.X; x < rect.X + rect.Width; x++)
            {
                result.SetTile(new Vector2(x, rect.Y), tileId);
                result.SetTile(new Vector2(x, rect.Y + rect.Height - 1), tileId);
            }
            for (var y = rect.Y; y < rect.Y + rect.Height; y++)
            {
                result.SetTile(new Vector2(rect.X, y), tileId);
                result.SetTile(new Vector2(rect.X + rect.Width - 1, y), tileId);
            }

            return result;
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
