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
                new TreeMazeGenerator.Settings
                {
                    Width = 21,
                    Height = 21,
                    Random = new Random(0),
                    AdditionalPassages = 0,
                    RemoveDeadEnds = false
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

        [Test]
        public void TreeMazeGenerator_GenerateWithoutRoomsAndDeadEnds_EmptyMaze()
        {
            var result = this.target.Generate(
                new TreeMazeGenerator.Settings
                {
                    Width = 21,
                    Height = 21,
                    Random = new Random(0),
                    AdditionalPassages = 0,
                    RemoveDeadEnds = true
                });

            Assert.AreEqual(@"
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
#####################
############# #######
#####################
#####################
#####################
#####################
#####################
#####################
#####################
", MazePrinter.Print(result.Regions));
        }
    }
}
