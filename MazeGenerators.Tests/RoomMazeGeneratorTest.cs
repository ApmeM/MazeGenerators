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
###           #     #
### #  #### # #     #
### #       # #     #
### ##### # # #     #
###       #         #
#   ####### # #######
# # #     #         #
# # #     # ####### #
# # #     # #     # #
# # #     # #     # #
# #       #       # #
# # # #######     # #
# # #       #     # #
# # # ##### ####### #
#   # ##    #       #
# ### ## ##   ##### #
# ### #   # ####### #
# ### #   # ####### #
#     #             #
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

        [Test]
        public void RoomMazeGenerator_GenerateRoomsWithSizeLimit_AllRoomsShouldFitCriteria()
        {
            var result = this.target.Generate(
                new RoomMazeGenerator.Settings
                {
                    Width = 201,
                    Height = 201,
                    MinRoomSize = 10,
                    MaxRoomSize = 100,
                    MaxWidthHeightRoomSizeDifference = 20
                });
            foreach (var r in result.Rooms)
            {
                Assert.LessOrEqual(r.Width, 100);
                Assert.LessOrEqual(r.Height, 100);
                Assert.GreaterOrEqual(r.Width, 10);
                Assert.GreaterOrEqual(r.Height, 10);
                Assert.LessOrEqual(Math.Abs(r.Height - r.Width), 20);
            }
        }

        [Test]
        public void RoomMazeGenerator_RoomSizeValuesInvalid_ThrowsException()
        {
            Assert.Throws<Exception>(()=>this.target.Generate(
                new RoomMazeGenerator.Settings
                {
                    Width = 201,
                    Height = 201,
                    MinRoomSize = 1000,
                    MaxRoomSize = 100,
                    MaxWidthHeightRoomSizeDifference = 20
                }));
        }


        [Test]
        public void RoomMazeGenerator_GenerateRoomsWithEqualSizeValues_AllRoomsHaveEqualSizes()
        {
            var result = this.target.Generate(
                new RoomMazeGenerator.Settings
                {
                    Width = 201,
                    Height = 201,
                    MinRoomSize = 11,
                    MaxRoomSize = 11,
                    MaxWidthHeightRoomSizeDifference = 20
                });
            foreach (var r in result.Rooms)
            {
                Assert.AreEqual(r.Width, 11);
                Assert.AreEqual(r.Height, 11);
            }
        }

    }
}
