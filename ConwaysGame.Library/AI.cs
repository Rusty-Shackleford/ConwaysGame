using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGame.Library
{
    public class AI
    {
        public static CellState GetCellState(CellState currentState, int liveNeighbors)
        {
            switch (currentState)
            {
                case CellState.Alive:
                    if (liveNeighbors < 2)
                    {
                        return CellState.Dead;
                    }
                    if (liveNeighbors > 3)
                    {
                        return CellState.Dead;
                    }
                    break;
                case CellState.Dead:
                    if (liveNeighbors == 3)
                    {
                        return CellState.Alive;
                    }
                    break;
            }
            return currentState;
        }
    }

}
