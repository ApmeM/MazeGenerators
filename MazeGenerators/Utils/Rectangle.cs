namespace MazeGenerators.Utils
{
    public struct Rectangle
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Rectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public bool Intersects(Rectangle value)
        {
            return value.X < this.X + this.Width && this.X < value.X + value.Width && value.Y < this.Y + this.Height
                   && this.Y < value.Y + value.Height;
        }

        public override string ToString()
        {
            return $"{X}x{Y} {Width}x{Height}";
        }
    }
}