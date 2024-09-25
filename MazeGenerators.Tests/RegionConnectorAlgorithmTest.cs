namespace MazeGenerators.Tests
{
    using MazeGenerators;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RegionConnectorAlgorithmTest
    {
        [Test]
        public void GenerateConnectors_SingleArea_NothingChanged()
        {
            var r = new Random(0);
            var result = new Maze(3, 5).Parse(
"   \n" +
" . \n" +
" . \n" +
" . \n" +
"   \n");
            result.GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs, 10);
            Assert.AreEqual(
"   \n" +
" . \n" +
" . \n" +
" . \n" +
"   \n", result.Stringify());
            Assert.AreEqual(0, result.Junctions.Count);
        }

        [Test]
        public void GenerateConnectors_MultipleAreas_ConnectorGenerated()
        {
            var r = new Random(0);
            var result = new Maze(3, 5).Parse(
"   \n" +
" . \n" +
"   \n" +
" . \n" +
"   \n");
            result.GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs, 10);
            Assert.AreEqual(
"   \n" +
" . \n" +
" - \n" +
" . \n" +
"   \n", result.Stringify());
            Assert.AreEqual(1, result.Junctions.Count);
            Assert.AreEqual(new Vector2(1, 2), result.Junctions[0]);
        }

        [Test]
        public void GenerateConnectors_RandomConnectorsRequested_ConnectorsAdded()
        {
            var r = new Random(0);
            var result = new Maze(5, 5).Parse(
"     \n" +
" ... \n" +
"     \n" +
" ... \n" +
"     \n");
            result.GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs, 100);
            Assert.AreEqual(
"     \n" +
" ... \n" +
" - - \n" +
" ... \n" +
"     \n", result.Stringify());
            Assert.AreEqual(2, result.Junctions.Count);
            Assert.AreEqual(new Vector2(3, 2), result.Junctions[0]);
        }

        [Test]
        public void GenerateConnectors_UnconnectableRegions_DoNotConnect()
        {
            var r = new Random(0);
            var result = new Maze(5, 5).Parse(
" ... \n" +
"     \n" +
"     \n" +
" ... \n" +
"     \n");
            result.GenerateConnectors((max) => r.Next(max), DefaultDirections.CardinalDirs, 100);
            Assert.AreEqual(
" ... \n" +
"     \n" +
"     \n" +
" ... \n" +
"     \n", result.Stringify());
            Assert.AreEqual(result.Junctions.Count, 0);
        }
    }
}
