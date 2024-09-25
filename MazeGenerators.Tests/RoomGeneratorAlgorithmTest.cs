namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class RoomGeneratorAlgorithmTest
    {
        [Test]
        public void GenerateRooms_RoomSizeValuesInvalid_ThrowsException()
        {
            var r = new Random(0);
            Assert.Throws<Exception>(() =>
            {
                new Maze(201, 201).GenerateRooms((max) => r.Next(max), 100, 4, true, 1000, 100, 20);
            });
        }

        [Test]
        public void GenerateRooms_EqualSizeValues_AllRoomsHaveEqualSizes()
        {
            var r = new Random(0);
            var result = new Maze(201, 201).GenerateRooms((max) => r.Next(max), 100, 4, true, 11, 11, 20);

            foreach (var room in result.Rooms)
            {
                Assert.AreEqual(room.Width, 11);
                Assert.AreEqual(room.Height, 11);
            }
        }

        [Test]
        public void GenerateRooms_SizeLimit_AllRoomsShouldFitCriteria()
        {
            var r = new Random(0);
            var result = new Maze(201, 201).GenerateRooms((max) => r.Next(max), 100, 4, true, 10, 100, 20);

            foreach (var room in result.Rooms)
            {
                Assert.LessOrEqual(room.Width, 100);
                Assert.LessOrEqual(room.Height, 100);
                Assert.GreaterOrEqual(room.Width, 10);
                Assert.GreaterOrEqual(room.Height, 10);
                Assert.LessOrEqual(Math.Abs(room.Height - room.Width), 20);
            }
        }

        [Test]
        public void GenerateRooms_LimitNumberOfGeneratedRooms_CreatedRequiredNumber()
        {
            var r = new Random(0);
            var result = new Maze(201, 201).GenerateRooms((max) => r.Next(max), 100, 2, true, 10, 11, 20);

            Assert.AreEqual(2, result.Rooms.Count);
        }
    }
}
