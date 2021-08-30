using MazeGenerators.Common;
using MazeGenerators.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MazeGenerators.StringParser
{
    public class StringParserAlgorithm
    {
        public static void Parse(IStringParserResult result, IStringParserSettings settings)
        {
            var reader = new StringReader(settings.MazeText);
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
                        case ' ':
                            CommonAlgorithm.SetTile(result, pos, settings.MazeTileId);
                            break;
                        case '#':
                            CommonAlgorithm.RemoveTile(result, pos);
                            break;
                        default:
                            throw new Exception("Unexpected character.");
                    }
                }
            }
        }

        public static string Stringify(IStringParserResult result, IStringParserSettings settings)
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
                    else if (tile.HasValue)
                    {
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append("#");
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
