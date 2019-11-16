using NUnit.Framework;

namespace ConwaysGameOfLife.Tests
{
    public class CellTests
    {
        [Test]
        public void ConstructorThrowsArgumentNullExceptionWhenCountLivingNeighborsIsNull()
        {
            Assert.That(() => new Cell(null, false), Throws.ArgumentNullException);
            Assert.That(() => new Cell(null, true), Throws.ArgumentNullException);
        }

        [Test]
        public void ConstructorThrowsNotExceptionWhenCountLivingNeighborsIsSet()
        {
            Assert.That(new Cell(() => 1, false), Is.Not.Null);
            Assert.That(new Cell(() => 1, true), Is.Not.Null);
        }
        
        [Test]
        public void IsAliveInitializedDependentOnIsAliveParameter()
        {
            Assert.That(new Cell(() => 0, false).IsAlive, Is.False);
            Assert.That(new Cell(() => 0, true).IsAlive, Is.True);
        }
        
        [Test]
        public void CellDiesWhenNeighborsCountIsLowerThan2OrGreater3()
        {
            Assert.That(new Cell(() => 0, true).IsAliveInNextGeneration(), Is.False);
            Assert.That(new Cell(() => 1, true).IsAliveInNextGeneration(), Is.False);
            Assert.That(new Cell(() => 4, true).IsAliveInNextGeneration(), Is.False);
            Assert.That(new Cell(() => 5, true).IsAliveInNextGeneration(), Is.False);
            Assert.That(new Cell(() => 6, true).IsAliveInNextGeneration(), Is.False);
            Assert.That(new Cell(() => 7, true).IsAliveInNextGeneration(), Is.False);
            Assert.That(new Cell(() => 8, true).IsAliveInNextGeneration(), Is.False);
        }

        [Test]
        public void CellIsAliveWhenNeighborsCountIsEqualTo2AndIsAlive()
        {
            Assert.That(new Cell(() => 2, true).IsAliveInNextGeneration(), Is.True);
            Assert.That(new Cell(() => 2, false).IsAliveInNextGeneration(), Is.False);
        }

        [Test]
        public void CellIsAliveWhenNeighborsCountIsEqualTo3()
        {
            Assert.That(new Cell(() => 3, true).IsAliveInNextGeneration(), Is.True);
            Assert.That(new Cell(() => 3, false).IsAliveInNextGeneration(), Is.True);
        }
        
        [Test]
        public void CellIsAliveWhenNeighborsCountIsEqualTo2AndCellIsAlive()
        {
            Assert.That(new Cell(() => 2, true).IsAliveInNextGeneration(), Is.True);
            Assert.That(new Cell(() => 2, false).IsAliveInNextGeneration(), Is.False);
        }
    }
}
