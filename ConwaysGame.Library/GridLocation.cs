using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGame.Library
{
    public struct GridLocation : IEquatable<GridLocation>
    {
        public readonly int Row;
        public readonly int Col;

        public GridLocation(int row, int col)
        {
            Row = row;
            Col = col;
        }


        #region [ IEquatable ]
        public bool Equals(GridLocation other)
        {
            return Row == other.Row && Col == other.Col;
        }


        public override bool Equals(object obj)
        {
            GridLocation other;
            if (obj is GridLocation)
            {
                other = (GridLocation)(obj);
            }
            else
            {
                return false;
            }
            if (Row == other.Row && Col == other.Col)
            {
                return true;
            }
            return false;
        }


        public static bool operator ==(GridLocation a, GridLocation b)
        {
            if (a.Equals(b))
            {
                return true;
            }
            return false;
        }


        public static bool operator !=(GridLocation a, GridLocation b)
        {
            if (!a.Equals(b))
            {
                return true;
            }
            return false;
        }
        #endregion


        #region [ GetHashCode ]
        public override int GetHashCode()
        {
            var hashCode = 1084646500;
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Col.GetHashCode();
            return hashCode;
        }
        #endregion

    }
}
