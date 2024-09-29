namespace MazeGenerators.Tests
{
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class RemoveDeadEndsAlgorithmTest
    {
        [Test]
        public void RemoveDeadEnds_LinearMaze_AllRemoved()
        {
            var result = new Maze(3,5).Parse( 
"   \n" +
" . \n" +
" . \n" +
" . \n" +
"   \n");
            result.RemoveDeadEnds(DefaultDirections.CardinalDirs);
            Assert.AreEqual(
"   \n" +
"   \n" +
"   \n" +
"   \n" +
"   \n", result.Stringify());
        }

        [Test]
        public void RemoveDeadEnds_NoDeadEnd_NothingChanged()
        {
            var result = new Maze(5,5).Parse(
"     \n" +
" ... \n" +
" . . \n" +
" ... \n" +
"     \n");
            result.RemoveDeadEnds(DefaultDirections.CardinalDirs);
            Assert.AreEqual(
"     \n" +
" ... \n" +
" . . \n" +
" ... \n" +
"     \n", result.Stringify());
        }

        [Test]
        public void RemoveDeadEnds_MixedMaze_OnlyDeadendsRemoved()
        {
            var result = new Maze(5,5).Parse(
"     \n" +
" ... \n" +
" ..  \n" +
" ... \n" +
"     \n");
            result.RemoveDeadEnds(DefaultDirections.CardinalDirs);
            Assert.AreEqual(
"     \n" +
" ..  \n" +
" ..  \n" +
" ..  \n" +
"     \n", result.Stringify());
        }
    }
}
