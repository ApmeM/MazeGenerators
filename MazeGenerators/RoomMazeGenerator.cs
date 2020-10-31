namespace MazeGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MazeGenerators.Utils;

    /// The random dungeon generator.
    ///
    /// Starting with a stage of solid walls, it works like so:
    ///
    /// 1. Place a number of randomly sized and positioned rooms. If a room
    ///    overlaps an existing room, it is discarded. Any remaining rooms are
    ///    carved out.
    /// 2. Any remaining solid areas are filled in with mazes. The maze generator
    ///    will grow and fill in even odd-shaped areas, but will not touch any
    ///    rooms.
    /// 3. The result of the previous two steps is a series of unconnected rooms
    ///    and mazes. We walk the stage and find every tile that can be a
    ///    "connector". This is a solid tile that is adjacent to two unconnected
    ///    regions.
    /// 4. We randomly choose connectors and open them or place a door there until
    ///    all of the unconnected regions have been joined. There is also a slight
    ///    chance to carve a connector between two already-joined regions, so that
    ///    the dungeon isn't single connected.
    /// 5. The mazes will have a lot of dead ends. Finally, we remove those by
    ///    repeatedly filling in any open tile that's closed on three sides. When
    ///    this is done, every corridor in a maze actually leads somewhere.
    ///
    /// The end result of this is a multiply-connected dungeon with rooms and lots
    /// of winding corridors.
    ///
    /// See original here: https://github.com/munificent/hauberk/blob/db360d9efa714efb6d937c31953ef849c7394a39/lib/src/content/dungeon.dart
    /// Described here: https://journal.stuffwithstuff.com/2014/12/21/rooms-and-mazes/
    public class RoomMazeGenerator
    {
        public class Result
        {
            public List<Vector2> Junctions;

            public List<Rectangle> Rooms;

            public int?[,] Regions;

            public int? GetTile(Vector2 pos)
            {
                return this.Regions[(int)pos.X, (int)pos.Y];
            }

            public void SetTile(Vector2 pos, int regionId)
            {
                this.Regions[(int)pos.X, (int)pos.Y] = regionId;
            }

            public void RemoveTile(Vector2 pos)
            {
                this.Regions[(int)pos.X, (int)pos.Y] = null;
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

            public int NumRoomTries = 100;

            /// The inverse chance of adding a connector between two regions that have
            /// already been joined. Increasing this leads to more loosely connected
            /// dungeons.
            public int ExtraConnectorChance = 20;

            /// Increasing this allows rooms to be larger.
            public int RoomExtraSize = 0;

            public int WindingPercent = 0;
        }

        public Result Generate(Settings settings)
        {
            if (settings.Width % 2 == 0 || settings.Height % 2 == 0)
            {
                throw new Exception("The stage must be odd-sized.");
            }

            var result = new Result
            {
                Junctions = new List<Vector2>(),
                Rooms = new List<Rectangle>(),
                Regions = new int?[settings.Width, settings.Height]
            };

            var regionId = -1;
            for (var i = 0; i < settings.NumRoomTries; i++)
            {
                regionId++;
                if (!this.TryAddRooms(result, settings, regionId))
                {
                    regionId--;
                }
            }

            // Fill in all of the empty space with mazes.
            for (var y = 1; y < settings.Height; y += 2)
            {
                for (var x = 1; x < settings.Width; x += 2)
                {
                    var pos = new Vector2(x, y);
                    if (result.GetTile(pos).HasValue)
                        continue;
                    regionId++;
                    this.GrowMaze(result, settings, pos, regionId);
                }
            }

            regionId++;
            this.ConnectRegions(result, settings, regionId);
            this.RemoveDeadEnds(result, settings);

            return result;
        }

        /// Implementation of the "growing tree" algorithm from here:
        /// http://www.astrolog.org/labyrnth/algrithm.htm.
        private void GrowMaze(Result result, Settings settings, Vector2 start, int regionId)
        {
            var cells = new List<Vector2>();

            result.SetTile(start, regionId);

            cells.Add(start);

            Vector2? lastDir = null;

            while (cells.Count > 0)
            {
                var cell = cells[cells.Count - 1];

                // See which adjacent cells are open.
                var unmadeCells = new List<Vector2>();

                foreach (var dir in settings.Directions)
                {
                    if (this.CanCarve(result, cell, dir))
                        unmadeCells.Add(dir);
                }

                if (unmadeCells.Count != 0)
                {
                    // Based on how "windy" passages are, try to prefer carving in the
                    // same direction.
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
                    // No adjacent un carved cells.
                    cells.RemoveAt(cells.Count - 1);

                    // This path has ended.
                    lastDir = null;
                }
            }
        }

        /// Places rooms ignoring the existing maze corridors.
        private bool TryAddRooms(Result result, Settings settings, int regionId)
        {
            // Pick a random room size. The funny math here does two things:
            // - It makes sure rooms are odd-sized to line up with maze.
            // - It avoids creating rooms that are too rectangular: too tall and
            //   narrow or too wide and flat.
            // TODO: This isn't very flexible or tunable. Do something better here.
            var size = (settings.Random.Next(2 + settings.RoomExtraSize) + 1) * 2 + 1;
            var rectangularity = settings.Random.Next(1 + size / 2) * 2;
            var width = size;
            var height = size;
            if (settings.Random.Next(100) < 50)
            {
                width += rectangularity;
            }
            else
            {
                height += rectangularity;
            }

            var x = settings.Random.Next((result.Regions.GetLength(0) - width) / 2) * 2 + 1;
            var y = settings.Random.Next((result.Regions.GetLength(1) - height) / 2) * 2 + 1;

            var room = new Rectangle(x, y, width, height);

            var overlaps = false;
            foreach (var other in result.Rooms)
            {
                if (room.Intersects(other))
                {
                    overlaps = true;
                    break;
                }
            }

            if (overlaps)
                return false;

            result.Rooms.Add(room);

            for (var i1 = x; i1 < x + width; i1++)
            for (var j1 = y; j1 < y + height; j1++)
            {
                result.SetTile(new Vector2(i1, j1), regionId);
            }

            return true;
        }

        private void ConnectRegions(Result result, Settings settings, int connectorId)
        {
            // Find all of the tiles that can connect two (or more) regions.
            var connectorRegions = new Dictionary<Vector2, HashSet<int>>();
            for (var x = 0; x < result.Regions.GetLength(0); x++)
            for (var y = 0; y < result.Regions.GetLength(1); y++)
            {
                var pos = new Vector2(x, y);
                // Can't already be part of a region.
                if (result.GetTile(pos).HasValue)
                    continue;

                var tmpRegions = new HashSet<int>();
                foreach (var dir in settings.Directions)
                {
                    var loc = pos + dir;
                    if(!result.IsInRegion(loc))
                    {
                        continue;
                    }

                    var region = result.GetTile(loc);
                    if (region != null)
                        tmpRegions.Add(region.Value);
                }

                if (tmpRegions.Count < 2)
                    continue;

                connectorRegions[pos] = tmpRegions;
            }

            var connectors = connectorRegions.Keys.ToList();

            // Keep track of which regions have been merged. This maps an original
            // region index to the one it has been merged to.
            var merged = new int[connectorId];
            var openRegions = new HashSet<int>();
            for (var i = 0; i < connectorId; i++)
            {
                merged[i] = i;
                openRegions.Add(i);
            }

            // Keep connecting regions until we're down to one.
            while (openRegions.Count > 1)
            {
                var connector = connectors[settings.Random.Next(connectors.Count)];

                // Carve the connection.
                result.SetTile(connector, connectorId);
                result.Junctions.Add(connector);

                // Merge the connected regions. We'll pick one region (arbitrarily) and
                // map all of the other regions to its index.
                var tmpRegions = connectorRegions[connector].Select((region) => merged[region]).ToList();
                var dest = tmpRegions[0];
                var sources = tmpRegions.Skip(1).ToList();

                // Merge all of the affected regions. We have to look at *all* of the
                // regions because other regions may have previously been merged with
                // some of the ones we're merging now.
                for (var i = 0; i < connectorId; i++)
                {
                    if (sources.Contains(merged[i]))
                    {
                        merged[i] = dest;
                    }
                }

                // The sources are no longer in use.
                foreach (var source in sources)
                {
                    openRegions.Remove(source);
                }

                // Remove any connectors that aren't needed anymore.
                connectors.RemoveAll(
                    (pos) =>
                    {
                        // Don't allow connectors right next to each other.
                        if ((connector - pos).LengthSquared() < 4)
                            return true;

                        // If the connector no long spans different regions, we don't need it.
                        var tmpRegions2 = connectorRegions[pos].Select((region) => merged[region]).ToLookup(a => a, a => a);

                        if (tmpRegions2.Count > 1)
                            return false;

                        // This connecter isn't needed, but connect it occasionally so that the
                        // dungeon isn't singly-connected.
                        if (settings.Random.Next(100) < settings.ExtraConnectorChance)
                        {
                            result.SetTile(pos, connectorId);
                            result.Junctions.Add(pos);
                        }

                        return true;
                    });
            }
        }

        private void RemoveDeadEnds(Result result, Settings settings)
        {
            var done = false;

            while (!done)
            {
                done = true;

                for (var x = 0; x < result.Regions.GetLength(0); x++)
                for (var y = 0; y < result.Regions.GetLength(1); y++)
                {
                    var pos = new Vector2(x, y);
                    if (!result.GetTile(pos).HasValue)
                    {
                        continue;
                    }

                    // If it only has one exit, it's a dead end.
                    var exits = 0;
                    foreach (var dir in settings.Directions)
                    {
                        if (!result.IsInRegion(pos + dir))
                        {
                            continue;
                        }

                        if (!result.GetTile(pos + dir).HasValue)
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
                    result.RemoveTile(pos);
                }
            }
        }

        /// Gets whether or not an opening can be carved from the given starting
        /// [Cell] at [pos] to the adjacent Cell facing [direction]. Returns `true`
        /// if the starting Cell is in bounds and the destination Cell is filled
        /// (or out of bounds).
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