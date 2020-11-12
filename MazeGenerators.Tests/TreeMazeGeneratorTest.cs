namespace MazeGenerators.Tests
{
    using System;

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

            Assert.AreEqual(@"
#####################
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
", MazePrinter.Print(result.Regions));
        }
    }
}
