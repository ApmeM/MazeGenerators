using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MazeGenerators
{
    public static class StringParserAlgorithm
    {
        public static Maze Parse(this Maze result, string mazeText, int offsetX = 0, int offsetY = 0)
        {
            var reader = new StringReader(mazeText);
            for (var y = 0; y < result.Height; y++)
            {
                var line = reader.ReadLine();
                for (var x = 0; x < result.Width; x++)
                {
                    var value = line[x];
                    var pos = new Vector2(x + offsetX, y + offsetY);
                    switch (value)
                    {
                        case '-':
                            result.SetTile(pos, Tile.MazeTileId);
                            result.Junctions.Add(pos);
                            break;
                        case '.':
                            result.SetTile(pos, Tile.MazeTileId);
                            break;
                        case '#':
                            result.SetTile(pos, Tile.WallTileId);
                            break;
                        case ' ':
                            result.SetTile(pos, Tile.EmptyTileId);
                            break;
                        default:
                            throw new Exception("Unexpected character.");
                    }
                }
            }

            return result;
        }

        public static string Stringify(this Maze result)
        {
            var hash = new HashSet<Vector2>(result.Junctions);
            var sb = new StringBuilder();
            for (var y = 0; y < result.Height; y++)
            {
                for (var x = 0; x < result.Width; x++)
                {
                    var pos = new Vector2(x, y);
                    var tile = result.GetTile(pos);
                    if (hash.Contains(pos))
                    {
                        sb.Append("-");
                    }
                    else if (tile == Tile.WallTileId)
                    {
                        sb.Append("#");
                    }
                    else if (tile == Tile.EmptyTileId)
                    {
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append(".");
                    }
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
