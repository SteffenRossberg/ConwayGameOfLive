using System;

namespace ConwaysGameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();

            game.Start();

            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        Console.Clear();
                        return;
                    case ConsoleKey.Enter:
                        game.Start();
                        break;
                    case ConsoleKey.Spacebar:
                        game.Stop();
                        break;
                }
            }
        }
    }
}
