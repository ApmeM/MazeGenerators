namespace MazeGenerators.Tests
{
    using NUnit.Framework;
    using MazeGenerators;
    using System;

    [TestFixture]
    public class TreeMazeBuilderAlgorithmTest
    {
        [Test]
        public void GenerateField_InvalidHeight_ExceptionThrown()
        {
            var r = new Random(0);
            Assert.Throws<Exception>(() => { new Maze(3, 4).GrowMaze((max) => r.Next(max), 0); });
        }

        [Test]
        public void GenerateField_InvalidWidth_ExceptionThrown()
        {
            var r = new Random(0);
            Assert.Throws<Exception>(() => { new Maze(2, 5).GrowMaze((max) => r.Next(max), 0); });
        }

        [Test]
        public void GrowMaze_NoWinding_StrightLine()
        {
            var r = new Random(0);
            var result = new Maze(7, 7).GrowMaze((max) => r.Next(max), 0);
            Assert.AreEqual(
"       \n" +
" . ... \n" +
" . . . \n" +
" . . . \n" +
" .   . \n" +
" ..... \n" +
"       \n", result.Stringify());
        }

        [Test]
        public void GrowMaze_WindingAlways_TreeMaze()
        {
            var r = new Random(0);
            var result = new Maze(7, 7).GrowMaze((max) => r.Next(max), 100);
            Assert.AreEqual(
"       \n" +
" . ... \n" +
" .   . \n" +
" . ... \n" +
" . . . \n" +
" ... . \n" +
"       \n", result.Stringify());
        }

    }
}
