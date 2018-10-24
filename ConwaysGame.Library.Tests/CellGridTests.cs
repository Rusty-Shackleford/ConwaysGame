using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGame.Library.Tests
{
    [TestFixture]
    public class CellGridTests
    {
        [Test]
        public void GetCellsAt_OnlyFindsOne_Neighbor()
        {
            // Arrange
            bool AllSearchesReturnOne = true;
            CellGrid grid = new CellGrid(new Size(50, 50), Vector2.Zero);

            // Act
            for (int i = 0; i < grid.Grid.Count; i++)
            {
                GridLocation checkLocation = grid.Grid[i].Location;
                IEnumerable<Cell> cells = grid.GetCellAt(checkLocation);
                if (cells.Count() != 1)
                    AllSearchesReturnOne = false;
                if (cells.ElementAt(0).Location != checkLocation)
                    AllSearchesReturnOne = false;
            }

            // Assert
            Assert.AreEqual(AllSearchesReturnOne, true);
        }


        [Test]
        public void GetCellsAt_LazyCheck()
        {
            // Arrange
            CellGrid grid = new CellGrid(new Size(50, 50), Vector2.Zero);

            // Act:  We will mark the cell at index 5 as dead
            // then retrieve it using it's GridLocation
            grid.Grid[5].NextState = CellState.Dead;
            GridLocation location = grid.Grid[5].Location;
            IEnumerable<Cell> testCells = grid.GetCellAt(location);
            Cell testCell = testCells.ElementAt(0);

            // Assert
            // Cell State should be dead, since we set it above, and all NextCellStates
            // default to CellState.Unknown
            Assert.AreEqual(testCell.NextState, CellState.Dead);
        }


        [Test]
        public void FirstRow_FirstCol_3Neighbors()
        {
            // Arrange
            CellGrid grid = new CellGrid(new Size(50, 50), Vector2.Zero);
            Cell cell = grid.Grid[0];
            // Act

            // Assert
            Assert.AreEqual(cell.Neighbors.Count, 3);
        }


        [Test]
        public void FirstRow_LastCol_3Neighbors()
        {
            // Arrange
            CellGrid grid = new CellGrid(new Size(50, 50), Vector2.Zero);
            Cell cell = grid.GetCellAt(
                new GridLocation(grid.FirstRow, grid.LastCol))
                .ElementAt(0);


            // Act

            // Assert
            Assert.AreEqual(cell.Neighbors.Count, 3);
        }


        [Test]
        public void LastRow_FirstCol_3Neighbors()
        {
            // Arrange
            CellGrid grid = new CellGrid(new Size(50, 50), Vector2.Zero);
            Cell cell = grid.GetCellAt(
                new GridLocation(grid.LastRow, grid.FirstCol))
                .ElementAt(0);
            // Act

            // Assert
            Assert.AreEqual(cell.Neighbors.Count, 3);
        }


        [Test]
        public void LastRow_LastCol_3Neighbors()
        {
            // Arrange
            CellGrid grid = new CellGrid(new Size(50, 50), Vector2.Zero);

            // Act
            Cell cell = grid.GetCellAt(
                new GridLocation(grid.LastRow, grid.LastCol))
                .ElementAt(0);
            // Assert
            Assert.AreEqual(cell.Neighbors.Count, 3);
        }


        //[Test]
        //public void GridUpdate_TTU_True()
        //{
        //    CellGrid grid = new CellGrid(new Size(50, 50), Vector2.Zero);
        //    GameTime g = new GameTime();
            
        //    grid.Update(g);
        //    Assert.AreEqual(CellState.Alive, grid.Grid[0].State);
        //}


    }
}
