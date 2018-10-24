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

        private Timer UpdateTimer;
        private bool TimeToUpdate;
        public int GenerationCount { get; private set; }
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

            // Setup Timer to inform every 1 minute
            UpdateTimer = new Timer(3);
            UpdateTimer.Completed += OnTimerCompleted;
        }
        #endregion


        #region [ SetupGrid ]
        private void SetupGrid()
        {
            Random rand = new Random();
            float x = Position.X;
            float y = Position.Y;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Cell cell = new Cell(new Vector2(x, y), i, j);
                    cell.Initialize(rand);
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
            UpdateTimer.Update(gameTime);

            if (TimeToUpdate)
            {
                TimeToUpdate = false;
                // someList.ForEach(x => { if(x.RemoveMe) someList.Remove(x); }); 

                for (int i = 0; i < Grid.Count; i++)
                {
                    Grid[i].NextState = AI.GetCellState(Grid[i].State, Grid[i].LivingNeighbors);
                }

                Grid.ForEach(cell =>
                {
                    cell.State = cell.NextState;
                });
                GenerationCount++;
            }

        }


        private void OnTimerCompleted(object sender, EventArgs e)
        {
            UpdateTimer.Restart();
            TimeToUpdate = true;
        }
        #endregion


    }
}
