using System;

namespace ConwaysGameOfLife
{
    public class Cell
    {
        private readonly Func<int> _countLivingNeighbors;
        public bool IsAlive { get; set; }

        public Cell(Func<int> countLivingNeighbors, bool isAlive)
        {
            _countLivingNeighbors = countLivingNeighbors ?? throw new ArgumentNullException();
            IsAlive = isAlive;
        }

        public bool IsAliveInNextGeneration()
        {
            var count = _countLivingNeighbors();
            return (IsAlive && count == 2) || count == 3;
        }
    }
}
