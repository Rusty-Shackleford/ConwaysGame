using ConwaysGame.Library;
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
    public class Cell
    {
        #region [ Members ]
        public CellState State { get; set; }
        public CellState NextState { get; set; }
        public Vector2 TexturePosition { get; set; }
        public List<Cell> Neighbors { get; set; }
        public GridLocation Location { get; private set; }
        private Timer LifeClock;
        private int StateChanges;

        #region [ LiveNeighborCount ]
        public int LivingNeighbors
        {
            get
            {
                int total = 0;
                for (int i = 0; i < Neighbors.Count; i++)
                {
                    if (Neighbors[i].State == CellState.Alive)
                    {
                        total++;
                    }
                }
                return total;
            }
        }
        #endregion

        #region [ Neighbor GridLocations ]
        public GridLocation TopLeftNeighbor
        {
            get { return new GridLocation(Location.Row - 1, Location.Col - 1); }
        }
        public GridLocation TopMiddleNeighbor
        {
            get { return new GridLocation(Location.Row - 1, Location.Col); }
        }
        public GridLocation TopRightNeighbor
        {
            get { return new GridLocation(Location.Row - 1, Location.Col + 1); }
        }
        public GridLocation LeftNeighbor
        {
            get { return new GridLocation(Location.Row, Location.Col - 1); }
        }
        public GridLocation RightNeighbor
        {
            get { return new GridLocation(Location.Row, Location.Col + 1); }
        }
        public GridLocation BottomLeftNeighbor
        {
            get { return new GridLocation(Location.Row + 1, Location.Col - 1); }
        }
        public GridLocation BottomMiddleNeighbor
        {
            get { return new GridLocation(Location.Row + 1, Location.Col); }
        }
        public GridLocation BottomRightNeighbor
        {
            get { return new GridLocation(Location.Row + 1, Location.Col + 1); }
        }

        #endregion
        #endregion


        #region [ Constructor ]
        public Cell(Vector2 position, int gridRow, int gridCol)
        {
            TexturePosition = position;
            Location = new GridLocation(gridRow, gridCol);
        }
        #endregion


        #region [ Initialize ]
        public void Initialize(Random rand)
        {
            int coin = rand.Next(1, 20);
            if (coin > 10)
            {
                NextState = CellState.Alive;
                State = CellState.Alive;
            }
            else
            {
                NextState = CellState.Dead;
                State = CellState.Dead;
            }
                

            double interval = rand.NextDouble(1, 3);
            LifeClock = new Timer(interval);

            LifeClock.Completed += OnLifeUpdate;
        }
        #endregion


        #region [ UpdateLife ]
        private void OnLifeUpdate(object sender, EventArgs e)
        {
            NextState = AI.GetCellState(State, LivingNeighbors);
            LifeClock.Restart();
            StateChanges++;
           // Debug.Write($"{ToString()} Update to: {NextState.ToString()}");
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            LifeClock.Update(gameTime);
            State = NextState;
        }
        #endregion


        #region [ ToString ]
        public override string ToString()
        {
            return $"[Cell @ {Location.ToString()}]: {State.ToString()} " +
                $"State Changes: {StateChanges.ToString()}";
        }
        #endregion

    }
}
