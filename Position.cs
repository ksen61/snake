using Pract9;

namespace Pract9
{
    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsEqual(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public static Position GenerateRandom()
        {
            Random rand = new Random();
            int x = rand.Next(1, (int)Pole.Width - 1);
            int y = rand.Next(1, (int)Pole.Height - 1);
            return new Position(x, y);
        }
    }
}

