namespace MazeGenerators.Tests
{
    using System;
    using System.Text;

    using NUnit.Framework;

    [TestFixture]
    public class TreeMazeGeneratorTest
    {
        private TreeMazeGenerator target;

        [SetUp]
        public void Setup()
        {
            this.target = new TreeMazeGenerator();
        }

        [Test]
        public void TreeMazeGenerator_Generate_SomeMaze()
        {
            var result = this.target.Generate(
                new TreeMazeGenerator.Settings(21, 21)
                {
                    Random = new Random(0)
                });

            Assert.AreEqual(@"#####################
# #     #         # #
# # ### # ### ### # #
# #   # #   # # #   #
# ### # # ### # ### #
#   # # # #   #   # #
### # # ### ##### # #
# # # #     #     # #
# # # ####### ##### #
# #   #     #   #   #
# ##### ### # # # ###
#       # # # # # # #
# ### ### # # # # # #
# #   #   # # #   # #
# # ### ### # ##### #
# #   #     # #     #
# ### ####### ##### #
# #   #     # #     #
# # ### ### # # ### #
# #       #     #   #
#####################
", this.Print(result.Regions));
        }

        public string Print(int?[,] data)
        {
            var sb = new StringBuilder();
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
