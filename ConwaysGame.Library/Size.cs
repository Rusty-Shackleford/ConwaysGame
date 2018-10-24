using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGame.Library
{
    public struct Size : IEquatable<Size>
    {
        #region [ Members ]
        public int Width;
        public int Height;
        public static Size Zero { get { return new Size(0, 0); } }
        #endregion


        #region [ Constructor ]
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
        #endregion


        #region [ IEquatable ]
        public bool Equals(Size other)
        {
            return Width == other.Width && Height == other.Height;
        }
        #endregion


        #region [ ToString ]
        public override string ToString()
        {
            return $"Height: {Height}, Width: {Width}";
        }
        #endregion
    }
}
