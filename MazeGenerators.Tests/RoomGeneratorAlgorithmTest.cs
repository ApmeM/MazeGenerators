namespace MazeGenerators.Tests
{
    using System;
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class RoomGeneratorAlgorithmTest
    {
        [Test]
        public void GenerateRooms_MinRoomSizeMoreThenMaxRoomSize_ThrowsException()
        {
            var r = new Random(0);
            Assert.Throws<Exception>(() =>
            {
                new Maze(201, 201).TryAddRoom((max) => r.Next(max), true, 1000, 100, 20);
            });
        }

        [Test]
        public void GenerateRooms_EqualSizeValues_AllRoomsHaveEqualSizes()
        {
            var r = new Random(0);
            var result = new Maze(201, 201).TryAddRoom((max) => r.Next(max), true, 11, 11, 20);

            foreach (var room in result.RoomToCells)
            {
                Assert.AreEqual(room.Count, 121);
            }
        }

        [Test]
        public void GenerateRooms_SizeLimit_AllRoomsShouldFitCriteria()
        {
            var r = new Random(0);
            var result = new Maze(201, 201).TryAddRoom((max) => r.Next(max), true, 10, 100, 20);

            foreach (var room in result.RoomToCells)
            {
                Assert.LessOrEqual(room.Count, 100*100);
                Assert.GreaterOrEqual(room.Count, 10*10);
            }
        }

        [Test]
        public void GenerateRooms_LimitNumberOfGeneratedRooms_CreatedRequiredNumber()
        {
            var r = new Random(0);
            var result = new Maze(201, 201)
                .TryAddRoom((max) => r.Next(max), true, 10, 11, 20)
                .TryAddRoom((max) => r.Next(max), true, 10, 11, 20);

            Assert.AreEqual(2, result.RoomToCells.Count);
        }
    }
}
