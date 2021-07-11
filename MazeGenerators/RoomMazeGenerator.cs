namespace MazeGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MazeGenerators.Utils;
    using MazeGenerators.Utils.DeadendRemover;
    using MazeGenerators.Utils.RegionConnector;

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
        public class Result : IDeadEndRemoverResult, IRegionConnectorResult, ITreeMazeBuilderResult
        {
            /// <summary>
            /// Actual generated maze data
            /// </summary>
            public int?[,] Regions;

            /// <summary>
            /// Junctions between different branches of a tree maze.
            /// Output number depens on <see cref="Settings.ExtraConnectorChance"/>.
            /// </summary>
            public List<Vector2> Junctions { get; set; }

            /// <summary>
            /// List of generated rooms.
            /// Output number depends on <see cref="Settings.NumRoomTries"/>
            /// </summary>
            public List<Rectangle> Rooms;

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

        public class Settings : IDeadEndRemoverSettings, IRegionConnectorSettings, ITreeMazeBuilderSettings
        {
            /// <summary>
            /// Width of the maze. 
            /// Must be odd-sized. 
            /// Left and Right columns will always contains walls.
            /// </summary>
            public int Width { get; set; } = 21;

            /// <summary>
            /// Height of the maze. 
            /// Must be odd-sized. 
            /// Left and Right columns will always contains walls.
            /// </summary>
            public int Height { get; set; } = 21;

            /// <summary>
            /// Random generator. 
            /// You can change it to your system wide random. 
            /// Or set with specified seed to make it more predictable.
            /// </summary>
            public Random Random { get; set; } = new Random();

            /// <summary>
            /// Possible directions when building paths across maze.
            /// Can be <see cref="Utils.Directions.CardinalDirs"/>, <see cref="Utils.Directions.CompassDirs"/> or any custom array of normalized vectors.
            /// </summary>
            public Vector2[] Directions { get; set; } = Utils.Directions.CardinalDirs;

            /// <summary>
            /// Number of tries to add a room.
            /// Number of rooms will probably be less then this number.
            /// - When <see cref="this.PreventOverlappedRooms"/> is true - some tries will fail.
            /// - When <see cref="this.PreventOverlappedRooms"/> is false - some rooms will contain eachother and will be invisible.
            /// </summary>
            public int NumRoomTries = 100;

            /// <summary>
            /// Specify if rooms overlapping is prevented or not.
            /// </summary>
            public bool PreventOverlappedRooms = true;

            /// <summary>
            /// Specify room size.
            /// Size become random from <see cref="MinRoomSize"/> to <see cref="MaxRoomSize"/> and made odd-sized.
            /// With width/height difference not bigger then <see cref="MaxWidthHeightRoomSizeDifference"/>
            /// </summary>
            public int MinRoomSize = 2;

            /// <summary>
            /// Specify room size.
            /// Size become random from <see cref="MinRoomSize"/> to <see cref="MaxRoomSize"/> and made odd-sized.
            /// With width/height difference not bigger then <see cref="MaxWidthHeightRoomSizeDifference"/>
            /// </summary>
            public int MaxRoomSize = 5;

            /// <summary>
            /// Specify maximum difference between room width and height to prevent long narrow rooms.
            /// Size become random from <see cref="MinRoomSize"/> to <see cref="MaxRoomSize"/> and made odd-sized.
            /// With width/height difference not bigger then <see cref="MaxWidthHeightRoomSizeDifference"/>
            /// </summary>
            public int MaxWidthHeightRoomSizeDifference = 5;

            /// <summary>
            /// Chance to turn direction during tree maze generation between rooms.
            /// The less this value the longer stright paths will be generated.
            /// </summary>
            public int WindingPercent { get; set; } = 0;

            /// <summary>
            /// Specify The number of additional passages to make maze not single-connected.
            ///  Increasing this leads to more loosely connected maze.
            /// </summary>
            public int AdditionalPassagesTries { get; set; } = 10;

            /// <summary>
            /// Specify if deadends from tree maze generation should be removed.
            /// </summary>
            public bool RemoveDeadEnds { get; set; } = true;
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
                    TreeMazeBuilderAlgorythm.GrowMaze(result, settings, pos, regionId);
                }
            }

            regionId++;
            this.ConnectRegions(result, settings, regionId);
            RegionConnectorAlgorythm.ConnectRegions(result, settings, regionId);
            DeadEndRemoverAlgorythm.RemoveDeadEnds(result, settings);

            return result;
        }

        /// Places rooms ignoring the existing maze corridors.
        private bool TryAddRooms(Result result, Settings settings, int regionId)
        {
            var minRoomSize = settings.MinRoomSize / 2 * 2 + 1;
            var maxRoomSize = (settings.MaxRoomSize + 1) / 2 * 2 - 1;
            var roomLength = maxRoomSize - minRoomSize;

            if (roomLength < 0)
            {
                throw new Exception("MaxRoomSize cant be less then MinRoomSize.");
            }

            var width = (settings.Random.Next(roomLength + 1) + minRoomSize) / 2 * 2 + 1;
            var maxDifference = settings.Random.Next(Math.Min(settings.MaxWidthHeightRoomSizeDifference, roomLength));
            var height = Math.Min(maxRoomSize, Math.Max(minRoomSize, (maxDifference - maxDifference / 2 + width) / 2 * 2 + 1));

            var x = settings.Random.Next((result.Regions.GetLength(0) - width) / 2) * 2 + 1;
            var y = settings.Random.Next((result.Regions.GetLength(1) - height) / 2) * 2 + 1;

            var room = new Rectangle(x, y, width, height);

            var overlaps = false;
            if (settings.PreventOverlappedRooms)
            {
                foreach (var other in result.Rooms)
                {
                    if (room.Intersects(other))
                    {
                        overlaps = true;
                        break;
                    }
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

                        return true;
                    });
            }
        }
    }
}