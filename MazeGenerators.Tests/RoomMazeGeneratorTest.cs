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
        public void RoomMazeGenerator_Generate_SomeMaze()
        {
            var result = this.target.Generate(
                new RoomMazeGenerator.Settings
                {
                    Width = 21,
                    Height = 21,
                    Random = new Random(0),
                    NumRoomTries = 10
                });

            Assert.AreEqual(@"
#####################
#                   #
# #########  ###### #
#       #           #
# ##### #     # ### #
#       #     # ### #
# ### # #     # ### #
#     # #     # ### #
#### ## #     # ### #
#     # #     #     #
#     # #     # #####
#     # #     # #   #
#     # ####### #   #
#     #       # #   #
#     # ##### # ### #
#     #       # ### #
#     # ## ## # ### #
#       #   # # ### #
###  ## #   # # ### #
###     #   #       #
#####################
", MazePrinter.Print(result.Regions));
        }

        [Test]
        public void RoomMazeGenerator_GenerateWithoutRooms_SameAsTreeMazeGenerator()
        {
            var result = this.target.Generate(
                new RoomMazeGenerator.Settings
                {
                    Width = 21,
                    Height = 21,
                    Random = new Random(0),
                    AdditionalPassages = 0,
                    RemoveDeadEnds = false,
                    NumRoomTries = 0,
                    WindingPercent = 100
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
