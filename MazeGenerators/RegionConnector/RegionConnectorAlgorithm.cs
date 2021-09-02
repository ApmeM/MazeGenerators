using MazeGenerators.Common;
using MazeGenerators.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerators.RegionConnector
{
    public class RegionConnectorAlgorithm
    {
        public static void GenerateConnectors(IRegionConnectorResult result, IRegionConnectorSettings settings)
        {
            var possibleConnectors = GetPossibleConnectorPositions(result, settings);

            ConnectRegions(result, settings, possibleConnectors);
            AddRandomConnectors(result, settings, possibleConnectors);
        }

        private static void AddRandomConnectors(IRegionConnectorResult result, IRegionConnectorSettings settings, HashSet<Vector2> possibleConnectors)
        {
            for (var i = 0; i < settings.AdditionalPassagesTries; i++)
            {
                if (possibleConnectors.Count == 0)
                {
                    break;
                }

                var pos = possibleConnectors.Skip(settings.Random.Next(possibleConnectors.Count)).First();
                foreach (var dir in settings.Directions)
                {
                    var loc = pos + dir;
                    // Do not allow 2 connectors next to each other.
                    possibleConnectors.Remove(loc);
                }
                possibleConnectors.Remove(pos);

                CommonAlgorithm.SetTile(result, pos, settings.JunctionTileId);
                result.Junctions.Add(pos);
            }
        }

        private static void ConnectRegions(IRegionConnectorResult result, IRegionConnectorSettings settings, HashSet<Vector2> possibleConnectors)
        {
            var regions = new int?[settings.Width, settings.Height];
            var regionId = 0;

            // Find all unconnected regions and assign numbers to them.
            for (var x = 0; x < settings.Width; x++)
                for (var y = 0; y < settings.Height; y++)
                {
                    var colored = FloodFill(result, settings, regions, new Vector2(x, y), regionId);
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
                foreach (var dir in settings.Directions)
                {
                    var loc = pos + dir;
                    if (!CommonAlgorithm.IsInRegion(result, loc))
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
            while (openRegions.Count > 1)
            {
                var connector = connectors[settings.Random.Next(connectors.Count)];

                // Carve the connection.
                CommonAlgorithm.SetTile(result, connector, settings.JunctionTileId);
                result.Junctions.Add(connector);

                foreach (var dir in settings.Directions)
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

        private static bool FloodFill(IRegionConnectorResult result, IRegionConnectorSettings settings, int?[,] regions, Vector2 pos, int color)
        {
            if (!CommonAlgorithm.IsInRegion(result, pos))
            {
                return false;
            }

            if (regions[pos.X, pos.Y].HasValue)
            {
                return false;
            }

            if (!result.Paths[pos.X, pos.Y].HasValue)
            {
                return false;
            }

            regions[pos.X, pos.Y] = color;
            foreach (var dir in settings.Directions)
            {
                FloodFill(result, settings, regions, pos + dir, color);
            }

            return true;
        }

        private static HashSet<Vector2> GetPossibleConnectorPositions(IRegionConnectorResult result, IRegionConnectorSettings settings)
        {
            var connectorRegions = new HashSet<Vector2>();
            for (var x = 0; x < settings.Width; x++)
                for (var y = 0; y < settings.Height; y++)
                {
                    var pos = new Vector2(x, y);

                    if (CommonAlgorithm.GetTile(result, pos).HasValue)
                    {
                        continue;
                    }

                    var found = false;
                    foreach (var dir in settings.Directions)
                    {
                        var loc1 = pos + dir;
                        var loc2 = pos - dir;
                        if (!CommonAlgorithm.IsInRegion(result, loc1) || !CommonAlgorithm.IsInRegion(result, loc2))
                        {
                            continue;
                        }

                        var region1 = CommonAlgorithm.GetTile(result, loc1);
                        var region2 = CommonAlgorithm.GetTile(result, loc2);
                        if (region1 != null && region2 != null)
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
