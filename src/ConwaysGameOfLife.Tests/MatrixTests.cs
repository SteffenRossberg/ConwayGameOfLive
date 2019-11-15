using System.Collections.Generic;
using NUnit.Framework;

namespace ConwaysGameOfLife.Tests
{
    public class MatrixTests
    {
        [Test]
        public void ConstructorThrowsArgumentExceptionWhenRowAndColumnCountAreLowerThan1()
        {
            Assert.That(() => new Matrix(0, 1), Throws.ArgumentException);
            Assert.That(() => new Matrix(1, 0), Throws.ArgumentException);
            Assert.That(() => new Matrix(-1, 1), Throws.ArgumentException);
            Assert.That(() => new Matrix(1, -1), Throws.ArgumentException);
            Assert.That(() => new Matrix(-2, 1), Throws.ArgumentException);
            Assert.That(() => new Matrix(1, -2), Throws.ArgumentException);
        }

        [Test]
        public void ConstructorThrowsNotExceptionWhenRowAndColumnCountAreGreaterThan0()
        {
            var sut = new Matrix(1, 1);
        }

        [Test]
        public void IsAliveReturnsFalse()
        {
            var sut = new Matrix(2, 2);
            
            Assert.That(sut.IsAlive(0, 0), Is.False);
            Assert.That(sut.IsAlive(1, 0), Is.False);
            Assert.That(sut.IsAlive(0, 1), Is.False);
            Assert.That(sut.IsAlive(1, 1), Is.False);
        }

        [Test]
        public void LoadLoadsCellsFromSource()
        {
            var sut = new Matrix(2, 2);
            var expected = new List<(int row, int column)>
            {
                (0, 0),
                (1, 1)
            };

            sut.Load(expected);

            Assert.That(sut.IsAlive(0, 0), Is.True);
            Assert.That(sut.IsAlive(1, 0), Is.False);
            Assert.That(sut.IsAlive(0, 1), Is.False);
            Assert.That(sut.IsAlive(1, 1), Is.True);
        }

        [Test]
        public void CountsLivingNeighbors()
        {
            var sut = new Matrix(3, 3);
            var expected = new List<(int row, int column)>
            {
                (0, 0),
                (0, 1),
                (0, 2),
                (1, 0),
                (1, 2),
                (2, 0),
                (2, 1),
            };
            sut.Load(expected);

            Assert.That(sut.CountLivingNeighbors(0, 0), Is.EqualTo(2));
            Assert.That(sut.CountLivingNeighbors(0, 1), Is.EqualTo(4));
            Assert.That(sut.CountLivingNeighbors(0, 2), Is.EqualTo(2));
            Assert.That(sut.CountLivingNeighbors(1, 0), Is.EqualTo(4));
            Assert.That(sut.CountLivingNeighbors(1, 1), Is.EqualTo(7));
            Assert.That(sut.CountLivingNeighbors(1, 2), Is.EqualTo(3));
            Assert.That(sut.CountLivingNeighbors(2, 0), Is.EqualTo(2));
            Assert.That(sut.CountLivingNeighbors(2, 1), Is.EqualTo(3));
            Assert.That(sut.CountLivingNeighbors(2, 2), Is.EqualTo(2));
        }

        [Test]
        public void UpdatesNextGeneration()
        {
            var sut = new Matrix(3, 3);
            var expected = new List<(int row, int column)>
            {
                (0, 0),
                (0, 1),
                (0, 2),
                (1, 0),
                (1, 2),
                (2, 0),
                (2, 1),
            };
            sut.Load(expected);

            sut.UpdateNextGeneration();

            Assert.That(sut.IsAlive(0, 0), Is.True);
            Assert.That(sut.IsAlive(0, 1), Is.False);
            Assert.That(sut.IsAlive(0, 2), Is.True);
            Assert.That(sut.IsAlive(1, 0), Is.False);
            Assert.That(sut.IsAlive(1, 1), Is.False);
            Assert.That(sut.IsAlive(1, 2), Is.True);
            Assert.That(sut.IsAlive(2, 0), Is.True);
            Assert.That(sut.IsAlive(2, 1), Is.True);
            Assert.That(sut.IsAlive(2, 2), Is.False);
        }

        [Test]
        public void GetsAllLivingCellsAfterLoad()
        {
            var sut = new Matrix(3, 3);
            var expected = new List<(int row, int column)>
            {
                (0, 0),
                (0, 1),
                (0, 2),
                (1, 0),
                (1, 2),
                (2, 0),
                (2, 1),
            };
            sut.Load(expected);

            var actual = sut.GetAllLivingCells();

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetsAllLivingCellsAfterUpdateNextGeneration()
        {
            var sut = new Matrix(3, 3);
            var initial = new List<(int row, int column)>
            {
                (0, 0),
                (0, 1),
                (0, 2),
                (1, 0),
                (1, 2),
                (2, 0),
                (2, 1),
            };
            sut.Load(initial);
            sut.UpdateNextGeneration();
            var expected = new List<(int row, int column)>
            {
                (0, 0),
                (0, 2),
                (1, 2),
                (2, 0),
                (2, 1),
            };


            var actual = sut.GetAllLivingCells();

            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}