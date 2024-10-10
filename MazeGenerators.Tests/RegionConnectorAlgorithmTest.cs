namespace MazeGenerators.Tests
{
    using MazeGenerators;
    using NUnit.Framework;

    [TestFixture]
    public class RegionConnectorAlgorithmTest
    {
        [Test]
        public void GenerateConnectors_SingleArea_NothingChanged()
        {
            var result = new Maze(3, 5).Parse(
"   \n" +
" . \n" +
" . \n" +
" . \n" +
"   \n");
            result.GenerateConnectors();
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
            var result = new Maze(3, 5).Parse(
"   \n" +
" . \n" +
"   \n" +
" . \n" +
"   \n");
            result.GenerateConnectors();
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
        public void GenerateConnectors_UnconnectableRegions_DoNotConnect()
        {
            var result = new Maze(5, 5).Parse(
" ... \n" +
"     \n" +
"     \n" +
" ... \n" +
"     \n");
            result.GenerateConnectors();
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
