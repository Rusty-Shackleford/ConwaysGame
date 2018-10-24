using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConwaysGame.Library
{
    public class CellGrid
    {
        #region [ Members ]
        public List<Cell> Grid { get; private set; }

        public int Cols { get; private set; } // X
        public int Rows { get; private set; } // Y
        public Size CellTextureSize { get; private set; }
        public int TotalCells
        {
            get { return Cols * Rows; }
        }
        public Vector2 Position { get; private set; }

        public int FirstRow = 0;
        public int LastRow { get { return Rows - 1; } }
        public int FirstCol = 0;
        public int LastCol { get { return Cols - 1; } }
        #endregion


        #region [ Constructor ]
        public CellGrid(Size cellTextureSize, Vector2 position, int cols = 10, int rows = 10)
        {
            CellTextureSize = cellTextureSize;
            Position = position;
            Cols = cols;
            Rows = rows;
            Grid = new List<Cell>();
            SetupGrid();

            // Find neighbors for each cell and store within each.
            foreach (Cell cell in Grid)
            {
                cell.Neighbors = GetNeighbors(cell);
            }
        }
        #endregion


        #region [ SetupGrid ]
        private void SetupGrid()
        {
            float x = Position.X;
            float y = Position.Y;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Cell cell = new Cell(new Vector2(x, y), i, j);
                    Grid.Add(cell);
                    //Debug.Print($"Row: {i} Col: {j}");
                    x += CellTextureSize.Width;
                }
                y += CellTextureSize.Height;
                x = Position.X;
            }
        }
        #endregion


        #region [ GetNeighbors ]
        public List<Cell> GetNeighbors(Cell cell)
        {
            List<Cell> neighbors = new List<Cell>();

            // Top
            if (Has_TopLeft_Neighbor(cell.Location))
                neighbors.Add(GetCellAt(cell.TopLeftNeighbor).ElementAt(0));

            if (Has_TopMiddle_Neighbor(cell.Location))
                neighbors.Add(GetCellAt(cell.TopMiddleNeighbor).ElementAt(0));

            if (Has_TopRight_Neighbor(cell.Location))
                neighbors.Add(GetCellAt(cell.TopRightNeighbor).ElementAt(0));

            // Middle
            if (Has_Left_Neighbor(cell.Location))
                neighbors.Add(GetCellAt(cell.LeftNeighbor).ElementAt(0));

            if (Has_Right_Neighbor(cell.Location))
                neighbors.Add(GetCellAt(cell.RightNeighbor).ElementAt(0));

            // Bottom
            if (Has_BottomLeft_Neighbor(cell.Location))
                neighbors.Add(GetCellAt(cell.BottomLeftNeighbor).ElementAt(0));

            if (Has_BottomMiddle_Neighbor(cell.Location))
                neighbors.Add(GetCellAt(cell.BottomMiddleNeighbor).ElementAt(0));

            if (Has_BottomRight_Neighbor(cell.Location))
                neighbors.Add(GetCellAt(cell.BottomRightNeighbor).ElementAt(0));

            return neighbors;
        }


        // HAS TOP
        public bool Has_TopLeft_Neighbor(GridLocation location)
        {
            if (location.Row != FirstRow && location.Col != FirstCol)
                return true;
            return false;
        }

        public bool Has_TopMiddle_Neighbor(GridLocation location)
        {
            if (location.Row != FirstRow)
                return true;
            return false;
        }

        private bool Has_TopRight_Neighbor(GridLocation location)
        {
            if (location.Row != FirstRow && location.Col != LastCol)
                return true;
            return false;
        }


        // HAS MIDDLE
        private bool Has_Left_Neighbor(GridLocation location)
        {
            if (location.Col != FirstCol)
                return true;
            return false;
        }

        private bool Has_Right_Neighbor(GridLocation location)
        {
            if (location.Col != LastCol)
                return true;
            return false;
        }


        // HAS BOTTOM
        private bool Has_BottomLeft_Neighbor(GridLocation location)
        {
            if (location.Row != LastRow && location.Col != FirstCol)
                return true;
            return false;
        }

        private bool Has_BottomMiddle_Neighbor(GridLocation location)
        {
            if (location.Row != LastRow)
                return true;
            return false;
        }

        private bool Has_BottomRight_Neighbor(GridLocation location)
        {
            if (location.Row != LastRow && location.Col != LastCol)
                return true;
            return false;
        }
        #endregion


        #region [ GetCellAt ]
        public IEnumerable<Cell> GetCellAt(GridLocation spot)
        {
            var matches = Grid.Where(cell =>
                spot.Row == cell.Location.Row &&
                spot.Col == cell.Location.Col
                );
            return matches;
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            //var cell = Grid[0];
            //cell.NextCellState = AI.GetCellState(cell.CellState, cell.LiveNeighborCount);
            //Debug.Write(cell.NextCellState.ToString());
            //cell.Update(gameTime);

            for (int i = 0; i < Grid.Count; i++)
            {
                var cell = Grid[i];
                cell.NextState = AI.GetCellState(cell.State, cell.LiveNeighborCount);
            }

            for (int i = 0; i < Grid.Count; i++)
            {
                Grid[i].Update(gameTime);
            }
        }
        #endregion

    }
}
