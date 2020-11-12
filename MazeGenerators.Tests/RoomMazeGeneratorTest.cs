namespace MazeGenerators.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class RoomMazeGeneratorTest
    {
        private RoomMazeGenerator target;

        [SetUp]
        public void Setup()
        {
            this.target = new RoomMazeGenerator();
        }

        [Test]
        public void TreeMazeGenerator_Generate_SomeMaze()
        {
            var result = this.target.Generate(
                new RoomMazeGenerator.Settings(21, 21)
                {
                    Random = new Random(0),
                    NumRoomTries = 10
                });

            Assert.AreEqual(@"
#####################
###########         #
########### ####### #
#       #     ##### #
# ##### #       ### #
# #####       # ### #
# ##### #     # ### #
#    ## #     # ### #
#### ## #     # ### #
#     #       #     #
#       #     # #####
#     # #     #     #
#     # ## ## # #   #
#             # #   #
#     # ##### # # ###
#     #    ## # #   #
#     # ## ## # ### #
#     # #   # # ### #
## #### #     # ### #
##      #   #       #
#####################
", MazePrinter.Print(result.Regions));
        }
    }
}
