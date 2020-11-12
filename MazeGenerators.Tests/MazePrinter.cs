using System.Text;

namespace MazeGenerators.Tests
{
    public class MazePrinter
    {
        public static string Print(int?[,] data)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            for (var y = 0; y < data.GetLength(1); y++)
            {
                for (var x = 0; x < data.GetLength(0); x++)
                {
                    if (data[x, y].HasValue)
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
