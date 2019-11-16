using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwaysGameOfLife
{
    public class Matrix
    {
        private readonly Cell[,] _cells;

        public Matrix(int rowCount, int columnCount)
        {
            if(rowCount < 1 || columnCount < 1) throw new ArgumentException($"{nameof(rowCount)} and {nameof(columnCount)} must be greater than 0.");
            _cells = new Cell[rowCount, columnCount];
            GetCells().ForAll(cell => _cells[cell.row, cell.column] = new Cell(() => CountLivingNeighbors(cell.row, cell.column), false));
        }

        public void Load(IEnumerable<(int row, int column)> livingCells)
            => livingCells
                .AsParallel()
                .ForAll(cell => _cells[cell.row, cell.column].IsAlive = true);

        public int CountLivingNeighbors(int row, int column)
            => GetCellCount(row - 1, column - 1)
               + GetCellCount(row - 1, column)
               + GetCellCount(row - 1, column + 1)
               + GetCellCount(row, column - 1)
               + GetCellCount(row, column + 1)
               + GetCellCount(row + 1, column - 1)
               + GetCellCount(row + 1, column)
               + GetCellCount(row + 1, column + 1);

        public void UpdateNextGeneration()
            => _cells
                .AsParallel()
                .OfType<Cell>()
                .Select(cell => (cell, isAliveInNextGeneration: cell.IsAliveInNextGeneration()))
                .Where(entry => entry.cell.IsAlive != entry.isAliveInNextGeneration)
                .ToList()
                .ForEach(entry => entry.cell.IsAlive = entry.isAliveInNextGeneration);

        public IEnumerable<(int row, int column)> GetAllLivingCells()
            => GetCells().Where(cell => IsAlive(cell.row, cell.column));

        public bool IsAlive(int row, int column)
            => _cells[row, column].IsAlive;

        private bool IsValidCell(int row, int column)
            => row >= 0
               && column >= 0
               && row < _cells.GetLength(0)
               && column < _cells.GetLength(1);

        private int GetCellCount(int row, int column)
            => IsValidCell(row, column) && _cells[row, column].IsAlive ? 1 : 0;

        private ParallelQuery<(int row, int column)> GetCells()
            => ParallelEnumerable
                .Range(0, _cells.GetLength(0))
                .SelectMany(row => ParallelEnumerable.Range(0, _cells.GetLength(1)).Select(column => (row, column)));
    }
}
