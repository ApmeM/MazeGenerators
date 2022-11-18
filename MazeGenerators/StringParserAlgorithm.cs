using MazeGenerators.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MazeGenerators
{
    public class StringParserAlgorithm
    {
        public static void Parse(GeneratorResult result, GeneratorSettings settings, string mazeText)
        {
            var reader = new StringReader(mazeText);
            for (var y = 0; y < settings.Height; y++)
            {
                var line = reader.ReadLine();
                for (var x = 0; x < settings.Width; x++)
                {
                    var value = line[x];
                    var pos = new Vector2(x, y);
                    switch (value)
                    {
                        case '-':
                            CommonAlgorithm.SetTile(result, pos, settings.JunctionTileId);
                            result.Junctions.Add(pos);
                            break;
                        case '.':
                            CommonAlgorithm.SetTile(result, pos, settings.MazeTileId);
                            break;
                        case '#':
                            CommonAlgorithm.SetTile(result, pos, settings.WallTileId);
                            break;
                        case ' ':
                            CommonAlgorithm.SetTile(result, pos, settings.EmptyTileId);
                            break;
                        default:
                            throw new Exception("Unexpected character.");
                    }
                }
            }
        }

        public static string Stringify(GeneratorResult result, GeneratorSettings settings)
        {
            var hash = new HashSet<Vector2>(result.Junctions);
            var sb = new StringBuilder();
            for (var y = 0; y < settings.Height; y++)
            {
                for (var x = 0; x < settings.Width; x++)
                {
                    var pos = new Vector2(x, y);
                    var tile = CommonAlgorithm.GetTile(result, pos);
                    if (hash.Contains(pos))
                    {
                        sb.Append("-");
                    }
                    else if (tile == settings.WallTileId)
                    {
                        sb.Append("#");
                    }
                    else if (tile == settings.EmptyTileId)
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
