namespace Pract9
{
    public class Snake
    {
        private List<Position> body = new List<Position>();
        private Position food;
        private int direction = 0;
        private bool a = true;
        private bool b = false;
        private int maxLength;

        public Snake()
        {
            Console.Clear();
            maxLength = ((int)Pole.Width - 2) * ((int)Pole.Height - 2);
            int head_x = (int)Pole.Width / 2;
            int head_y = (int)Pole.Height / 2;
            body.Add(new Position(head_x, head_y));
            body.Add(new Position(head_x, head_y + 1));
            GenerateFood();
            DrawPole();
            DrawSnake(body);
            DrawFood();
        }

        public void StartGame()
        {
            a = true;
            b = false;
            ConsoleKeyInfo key = Console.ReadKey();
            Thread thread = new Thread(new ThreadStart(StartDrawing));
            thread.Start();
            while (a && !b)
            {
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (direction != 2)
                        {
                            direction = 0;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (direction != 3)
                        {
                            direction = 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (direction != 0)
                        {
                            direction = 2;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (direction != 1)
                        {
                            direction = 3;
                        }
                        break;
                }

                key = Console.ReadKey();
            }
        }

        private void DrawPole()
        {
            for (int i = 0; i < (int)Pole.Height; i++)
            {
                for (int j = 0; j < (int)Pole.Width; j++)
                {
                    if (i == 0 || j == 0 || i == (int)Pole.Height - 1 || j == (int)Pole.Width - 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("#");
                    }
                }
            }
        }

        private void DrawSnake(List<Position> old)
        {
            foreach (var elem in old)
            {
                Console.SetCursorPosition(elem.X, elem.Y);
                Console.Write(" ");
            }
            foreach (var elem in body)
            {
                Console.SetCursorPosition(elem.X, elem.Y);
                Console.Write("*");
            }
        }

        private void StartDrawing()
        {
            var oldBody = Copy();
            while (a && !b)
            {
                DrawSnake(oldBody);
                oldBody = Copy();
                Move();
                Thread.Sleep(100);
            }
        }

        private void DrawFood()
        {
            Console.SetCursorPosition(food.X, food.Y);
            Console.Write("x");
        }

        private void Move()
        {
            var head = body[0];
            Position new_head;
            switch (direction)
            {
                case 0:
                    new_head = new Position(head.X, head.Y - 1);
                    break;
                case 1:
                    new_head = new Position(head.X + 1, head.Y);
                    break;
                case 2:
                    new_head = new Position(head.X, head.Y + 1);
                    break;
                case 3:
                    new_head = new Position(head.X - 1, head.Y);
                    break;
                default:
                    new_head = new Position(head.X, head.Y);
                    break;
            }
            if (new_head.X < 1 || new_head.X > (int)Pole.Width - 2 || new_head.Y < 1 || new_head.Y > (int)Pole.Height - 2 || PositionInSnake(new_head))
            {
                a = false;
                return;
            }
            body.Insert(0, new_head);
            if (new_head.IsEqual(food))
            {
                GenerateFood();
                DrawFood();
                if (body.Count == maxLength)
                {
                    b = true;
                    return;
                }
            }
            else
            {
                body.RemoveAt(body.Count - 1);
            }
        }

        private void GenerateFood()
        {
            Position fuit_position = Position.GenerateRandom();
            while (PositionInSnake(fuit_position))
            {
                fuit_position = Position.GenerateRandom();
            }
            food = fuit_position;
        }

        private bool PositionInSnake(Position position)
        {
            foreach (var elem in body)
            {
                if (elem.IsEqual(position))
                {
                    return true;
                }
            }

            return false;
        }

        private List<Position> Copy()
        {
            List<Position> copy = new List<Position>();
            foreach (var elem in body)
            {
                copy.Add(new Position(elem.X, elem.Y));
            }
            return copy;
        }
    }
}
