using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerators
{
    public static class RegionConnectorAlgorithm
    {
        public static Maze GenerateConnectors(this Maze result, Func<int, int> nextRandom, Vector2[] directions, int additionalPassagesTries = 10)
        {
            var possibleConnectors = GetPossibleConnectorPositions(result, directions);

            ConnectRegions(result, nextRandom, possibleConnectors, directions);
            AddRandomConnectors(result, nextRandom, possibleConnectors, additionalPassagesTries, directions);
            return result;
        }

        private static void AddRandomConnectors(Maze result, Func<int, int> nextRandom, HashSet<Vector2> possibleConnectors, int additionalPassagesTries, Vector2[] directions)
        {
            for (var i = 0; i < additionalPassagesTries; i++)
            {
                if (possibleConnectors.Count == 0)
                {
                    break;
                }

                var pos = possibleConnectors.Skip(nextRandom(possibleConnectors.Count)).First();
                foreach (var dir in directions)
                {
                    var loc = pos + dir;
                    // Do not allow 2 connectors next to each other.
                    possibleConnectors.Remove(loc);
                }
                possibleConnectors.Remove(pos);

                result.SetTile(pos, Tile.JunctionTileId);
                result.Junctions.Add(pos);
            }
        }

        private static void ConnectRegions(Maze result, Func<int, int> nextRandom, HashSet<Vector2> possibleConnectors, Vector2[] directions)
        {
            var regions = new int?[result.Width, result.Height];
            var regionId = 0;

            // Find all unconnected regions and assign numbers to them.
            for (var x = 0; x < result.Width; x++)
                for (var y = 0; y < result.Height; y++)
                {
                    var colored = FloodFill(result, regions, new Vector2(x, y), regionId, directions);
                    if (colored)
                    {
                        regionId++;
                    }
                }

            // Find all of the tiles that can connect two (or more) regions.
            var regionConnectors = new Dictionary<Vector2, HashSet<int>>();
            foreach (var pos in possibleConnectors)
            {
                var tmpRegions = new HashSet<int>();
                foreach (var dir in directions)
                {
                    var loc = pos + dir;
                    if (!result.IsInRegion(loc))
                    {
                        continue;
                    }

                    var region = regions[loc.X, loc.Y];
                    if (region != null)
                        tmpRegions.Add(region.Value);
                }

                if (tmpRegions.Count < 2)
                    continue;

                regionConnectors[pos] = tmpRegions;
            }

            var connectors = regionConnectors.Keys.ToList();

            // Keep track of which regions have been merged. This maps an original
            // region index to the one it has been merged to.
            var merged = new int[regionId];
            var openRegions = new HashSet<int>();
            for (var i = 0; i < regionId; i++)
            {
                merged[i] = i;
                openRegions.Add(i);
            }

            // Keep connecting regions until we're down to one.
            while (openRegions.Count > 1 && connectors.Count > 0)
            {
                var connector = connectors[nextRandom(connectors.Count)];

                // Carve the connection.
                result.SetTile(connector, Tile.JunctionTileId);
                result.Junctions.Add(connector);

                foreach (var dir in directions)
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
                for (var i = 0; i < regionId; i++)
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

        private static bool FloodFill(Maze result, int?[,] regions, Vector2 pos, int color, Vector2[] directions)
        {
            if (!result.IsInRegion(pos))
            {
                return false;
            }

            if (regions[pos.X, pos.Y].HasValue)
            {
                return false;
            }

            if (result.Paths[pos.X, pos.Y] == Tile.EmptyTileId)
            {
                return false;
            }

            regions[pos.X, pos.Y] = color;
            foreach (var dir in directions)
            {
                FloodFill(result, regions, pos + dir, color, directions);
            }

            return true;
        }

        private static HashSet<Vector2> GetPossibleConnectorPositions(Maze result, Vector2[] directions)
        {
            var connectorRegions = new HashSet<Vector2>();
            for (var x = 0; x < result.Width; x++)
                for (var y = 0; y < result.Height; y++)
                {
                    var pos = new Vector2(x, y);

                    if (result.GetTile(pos) != Tile.EmptyTileId)
                    {
                        continue;
                    }

                    var found = false;
                    foreach (var dir in directions)
                    {
                        var loc1 = pos + dir;
                        var loc2 = pos - dir;
                        if (!result.IsInRegion(loc1) || !result.IsInRegion(loc2))
                        {
                            continue;
                        }

                        var region1 = result.GetTile(loc1);
                        var region2 = result.GetTile(loc2);
                        if (region1 != Tile.EmptyTileId && region2 != Tile.EmptyTileId)
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
