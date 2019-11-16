using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class Game
    {
        private Task _gameLoopTask;
        private CancellationTokenSource _cancellationTokenSource;
        private Matrix _matrix;
        private List<(int row, int column)> _oldLivingCells;
        private List<(int row, int column)> _newLivingCells;

        public Game()
        {
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;
            _gameLoopTask = Task.Run(() =>
            {
                Console.CursorVisible = false;
                InitNewGame();
                while (!token.IsCancellationRequested)
                {
                    Update();
                    Render();
                }
            }, _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _gameLoopTask.Wait();
            Console.CursorVisible = true;
            Console.Clear();
        }

        private void InitNewGame()
        {
            var rows = 40;
            var columns = 40;
            var random = new Random();
            _matrix = new Matrix(rows, columns);
            _matrix.Load(Enumerable.Range(0, (int)(rows * columns * 0.7)).Select(index => (random.Next(0, rows), random.Next(0, columns))));
            Console.Clear();
        }

        private void Update()
        {
            _oldLivingCells = _matrix.GetAllLivingCells().ToList();
            _matrix.UpdateNextGeneration();
            _newLivingCells = _matrix.GetAllLivingCells().ToList();
        }

        private void Render()
        {
            _oldLivingCells.ForEach(cell =>
                {
                    Console.SetCursorPosition(cell.column, cell.row);
                    Console.Write(" ");
                });
            _newLivingCells.ForEach(cell =>
                {
                    Console.SetCursorPosition(cell.column, cell.row);
                    Console.Write("#");
                });
        }
    }
}
