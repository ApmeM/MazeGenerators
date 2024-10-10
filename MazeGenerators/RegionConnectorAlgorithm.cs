using System.Collections.Generic;

namespace MazeGenerators
{
    public static class RegionConnectorAlgorithm
    {
        public static Maze GenerateConnectors(this Maze result)
        {
            var regions = result.CellToRegion;

            var createdConnections = new HashSet<(int, int)>();
            for (var x = 0; x < result.Width; x++)
                for (var y = 0; y < result.Height; y++)
                {
                    var pos = new Vector2(x, y);

                    if (result.GetTile(pos) == Tile.MazeTileId)
                    {
                        continue;
                    }

                    foreach (var dir in result.Directions)
                    {
                        if (result.GetTile(pos + dir) == Tile.MazeTileId && 
                            result.GetTile(pos - dir) == Tile.MazeTileId &&
                            regions.ContainsKey(pos + dir) && // This is required as newly created connections contains maze underneath but was not added to regions yet.
                            regions.ContainsKey(pos - dir) && // This is required as newly created connections contains maze underneath but was not added to regions yet.
                            regions[pos + dir] != regions[pos - dir] &&
                            !createdConnections.Contains((regions[pos + dir], regions[pos - dir])))
                        {
                            result.SetTile(pos, Tile.MazeTileId);
                            result.Junctions.Add(pos);

                            createdConnections.Add((regions[pos + dir], regions[pos - dir]));
                            createdConnections.Add((regions[pos - dir], regions[pos + dir]));
                            break;
                        }
                    }
                }
            return result;
        }
    }
}
