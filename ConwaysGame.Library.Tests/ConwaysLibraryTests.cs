using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConwaysGame.Library.Tests
{
    [TestFixture]
    public class ConwaysLibraryTests
    {
        // 1: Live Cell & < 2 Neighbors = DEAD
        // Any live cell with fewer than two live neighbors dies, as if caused by under-population.

        // 2: Live Cell & 2-3 Neighbors = ALIVE
        // Any live cell with two or three live neighbors lives on to the next generation.

        // 3: Live Cell & > 3 Neighbors = DEAD
        // Any live cell with more than three neighbors dies, as if by over-crowding.

        // 4: Dead Cell & = 3 Neighbors = ALIVE
        // Any dead cell with exactly three neighbors becomes a live cell, as if by reproduction.


        [Test]
        public void LiveCell_LT2Neighbor_DIES([Values(0, 1)] int liveNeighbors)
        {
            // ARRANGE
            CellState currentCellState = CellState.Alive;
            // ACT
            CellState newCellState = AI.GetCellState(currentCellState, liveNeighbors);
            // ASSERT
            Assert.AreEqual(newCellState, CellState.Dead);
        }


        [Test]
        public void LiveCell_2or3Neighbor_LIVES([Values(2, 3)] int liveNeighbors)
        {
            CellState currentCellState = CellState.Alive;
            CellState newCellState = AI.GetCellState(currentCellState, liveNeighbors);
            Assert.AreEqual(newCellState, CellState.Alive);
        }


        [Test]
        public void LiveCell_GT3Neighbor_DIES([Range(4, 8)] int liveNeighbors)
        {
            CellState currentCellState = CellState.Alive;
            CellState newCellState = AI.GetCellState(currentCellState, liveNeighbors);
            Assert.AreEqual(newCellState, CellState.Dead);
        }


        [Test]
        public void DeadCell_3Neighbor_LIVES()
        {
            int liveNeighbors = 3;
            CellState currentCellState = CellState.Dead;
            CellState newCellState = AI.GetCellState(currentCellState, liveNeighbors);
            Assert.AreEqual(newCellState, CellState.Alive);
        }

    }
}
