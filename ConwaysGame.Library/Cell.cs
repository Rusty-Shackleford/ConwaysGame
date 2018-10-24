using ConwaysGame.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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

        #region [ LiveNeighborCount ]
        public int LiveNeighborCount
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
            NextState = CellState.Unknown;
            State = RandState();
        }
        #endregion


        #region [ Initialize ]
        private CellState RandState()
        {
            Random rnd = new Random();
            int coin = rnd.Next(1, 25);
            if (coin > 10)
                return CellState.Alive;
            return CellState.Dead;
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            State = NextState;
            NextState = CellState.Unknown;
        }
        #endregion


        #region [ Draw ]
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, Position, Color.White);
        }
        #endregion


    }
}
