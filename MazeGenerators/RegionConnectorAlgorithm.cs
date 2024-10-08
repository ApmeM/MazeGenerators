using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerators
{
    public static class RegionConnectorAlgorithm
    {
        private static readonly Random r = new Random();
        private static readonly Func<int, int> defaultRandomizer = (max) => r.Next(max);

        public static Maze GenerateConnectors(this Maze result, Func<int, int> nextRandom = null, int additionalPassagesTries = 5)
        {
            nextRandom = nextRandom ?? defaultRandomizer;

            var possibleConnectors = GetPossibleConnectorPositions(result);

            ConnectRegions(result, nextRandom, possibleConnectors);
            AddRandomConnectors(result, nextRandom, possibleConnectors, additionalPassagesTries);
            return result;
        }

        private static void AddRandomConnectors(Maze result, Func<int, int> nextRandom, HashSet<Vector2> possibleConnectors, int additionalPassagesTries)
        {
            for (var i = 0; i < additionalPassagesTries; i++)
            {
                if (possibleConnectors.Count == 0)
                {
                    break;
                }

                var pos = possibleConnectors.Skip(nextRandom(possibleConnectors.Count)).First();
                foreach (var dir in result.Directions)
                {
                    var loc = pos + dir;
                    // Do not allow 2 connectors next to each other.
                    possibleConnectors.Remove(loc);
                }
                possibleConnectors.Remove(pos);

                result.SetTile(pos, Tile.MazeTileId);
                result.Junctions.Add(pos);
            }
        }

        private static void ConnectRegions(Maze result, Func<int, int> nextRandom, HashSet<Vector2> possibleConnectors)
        {
            var regions = result.CellToRegion;
            var roomsCount = result.RegionToCells.Count;
            // Find all of the tiles that can connect two (or more) regions.
            var regionConnectors = new Dictionary<Vector2, HashSet<int>>();
            foreach (var pos in possibleConnectors)
            {
                var tmpRegions = new HashSet<int>();
                foreach (var dir in result.Directions)
                {
                    var loc = pos + dir;
                    if (regions.ContainsKey(loc))
                        tmpRegions.Add(regions[loc]);
                }

                if (tmpRegions.Count < 2)
                    continue;

                regionConnectors[pos] = tmpRegions;
            }

            var connectors = regionConnectors.Keys.ToList();

            // Keep track of which regions have been merged. This maps an original
            // region index to the one it has been merged to.
            var merged = new int[roomsCount];
            var openRegions = new HashSet<int>();
            for (var i = 0; i < roomsCount; i++)
            {
                merged[i] = i;
                openRegions.Add(i);
            }

            // Keep connecting regions until we're down to one.
            while (openRegions.Count > 1 && connectors.Count > 0)
            {
                var connector = connectors[nextRandom(connectors.Count)];

                // Carve the connection.
                result.SetTile(connector, Tile.MazeTileId);
                result.Junctions.Add(connector);

                foreach (var dir in result.Directions)
                {
                    var loc = connector + dir;
                    // Do not allow 2 connectors next to each other.
                    possibleConnectors.Remove(loc);
                }
                possibleConnectors.Remove(connector);

                // Merge the connected regions. We'll pick one region (arbitrarily) and
                // map all of the other regions to its index.
                var tmpRegions = regionConnectors[connector].Select((region) => merged[region]).ToList();
                var dest = tmpRegions[0];
                var sources = tmpRegions.Skip(1).ToList();

                // Merge all of the affected regions. We have to look at *all* of the
                // regions because other regions may have previously been merged with
                // some of the ones we're merging now.
                for (var i = 0; i < roomsCount; i++)
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
                        // If the connector no longer spans different regions, we don't need it.
                        return regionConnectors[pos].Select((region) => merged[region]).ToLookup(a => a, a => a).Count <= 1;
                    });
            }
        }

        private static HashSet<Vector2> GetPossibleConnectorPositions(Maze result)
        {
            var connectorRegions = new HashSet<Vector2>();
            for (var x = 0; x < result.Width; x++)
                for (var y = 0; y < result.Height; y++)
                {
                    var pos = new Vector2(x, y);

                    if (result.GetTile(pos) == Tile.MazeTileId)
                    {
                        continue;
                    }

                    var found = false;
                    foreach (var dir in result.Directions)
                    {
                        var region1 = result.GetTile(pos + dir);
                        var region2 = result.GetTile(pos - dir);
                        if (region1 == Tile.MazeTileId && region2 == Tile.MazeTileId)
                        {
                            found = true;
                        }
                    }

                    if (found)
                    {
                        connectorRegions.Add(pos);
                    }
                }

            return connectorRegions;
        }
    }
}
