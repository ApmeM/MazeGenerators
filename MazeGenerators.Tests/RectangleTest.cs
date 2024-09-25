namespace MazeGenerators.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class RectangleTest
    {
        private Rectangle target;

        [SetUp]
        public void Setup()
        {
            this.target = new Rectangle(10, 10, 10, 10);
        }

        [Test]
        public void Intersects_LeftBorder_True()
        {
            var result = this.target.Intersects(new Rectangle(5, 5, 10, 20));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_RightBorder_True()
        {
            var result = this.target.Intersects(new Rectangle(15, 5, 10, 20));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_TopBorder_True()
        {
            var result = this.target.Intersects(new Rectangle(5, 5, 20, 10));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_BottomBorder_True()
        {
            var result = this.target.Intersects(new Rectangle(5, 15, 20, 10));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_OutLeftBorder_False()
        {
            var result = this.target.Intersects(new Rectangle(-5, 5, 10, 20));

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Intersects_OutRightBorder_False()
        {
            var result = this.target.Intersects(new Rectangle(25, 5, 10, 20));

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Intersects_OutTopBorder_False()
        {
            var result = this.target.Intersects(new Rectangle(5, -5, 20, 10));

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Intersects_OutBottomBorder_False()
        {
            var result = this.target.Intersects(new Rectangle(5, 25, 20, 10));

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Intersects_TopLeftCorner_True()
        {
            var result = this.target.Intersects(new Rectangle(5, 5, 10, 10));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_TopRightCorner_True()
        {
            var result = this.target.Intersects(new Rectangle(15, 5, 10, 10));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_BottomLeftCorner_True()
        {
            var result = this.target.Intersects(new Rectangle(5, 15, 10, 10));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_BottomRightCorner_True()
        {
            var result = this.target.Intersects(new Rectangle(15, 15, 10, 10));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_Inside_True()
        {
            var result = this.target.Intersects(new Rectangle(5, 5, 20, 20));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_Outside_True()
        {
            var result = this.target.Intersects(new Rectangle(12, 12, 5, 5));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Intersects_OutTopLeftCorner_False()
        {
            var result = this.target.Intersects(new Rectangle(0, 0, 10, 10));

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Intersects_OutTopRightCorner_False()
        {
            var result = this.target.Intersects(new Rectangle(25, 5, 10, 10));

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Intersects_OutBottomLeftCorner_False()
        {
            var result = this.target.Intersects(new Rectangle(0, 25, 10, 10));

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Intersects_OutBottomRightCorner_False()
        {
            var result = this.target.Intersects(new Rectangle(25, 25, 10, 10));

            Assert.AreEqual(false, result);
        }
    }
}
