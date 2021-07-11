namespace MazeGenerators.Utils.RegionConnector
{
    internal class RegionConnectorAlgorythm
    {
        internal static void ConnectRegions(IRegionConnectorResult result, IRegionConnectorSettings settings, int connectorId)
        {
            for (var i = 0; i < settings.AdditionalPassagesTries; i++)
            {
                var pos = new Vector2(
                    settings.Random.Next(settings.Width - 2) + 1,
                    settings.Random.Next(settings.Height - 2) + 1);

                // Can't already be part of a region.
                if (result.GetTile(pos).HasValue)
                {
                    continue;
                }

                var found = false;
                foreach (var dir in settings.Directions)
                {
                    var loc1 = pos + dir;
                    var loc2 = pos - dir;
                    if (!result.IsInRegion(loc1) || !result.IsInRegion(loc2))
                    {
                        continue;
                    }

                    var region1 = result.GetTile(loc1);
                    var region2 = result.GetTile(loc2);
                    if (region1 != null && region2 != null)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    continue;
                }

                result.SetTile(pos, connectorId);
                result.Junctions.Add(pos);
            }
        }

    }
}
