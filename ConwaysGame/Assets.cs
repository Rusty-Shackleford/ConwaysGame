using ConwaysGame.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConwaysGame
{
    public static class Assets
    {
        public static Texture2D Cell_Alive { get; internal set; }
        public static Texture2D Cell_Dead { get; internal set; }
        public static Rectangle CellTextureSize { get; internal set; }
        public static Texture2D Background { get; internal set; }
        public static Texture2D MouseCursor { get; internal set; }

        public static void LoadContent(ContentManager content)
        {
            Cell_Alive = content.Load<Texture2D>(@"Cell_Alive_v3");
            Cell_Dead = content.Load<Texture2D>(@"Cell_Dead_v3");
            CellTextureSize = SetCellTextureSize();
            Background = content.Load<Texture2D>(@"Background");
            MouseCursor = content.Load<Texture2D>(@"MouseCursor");
        }

        private static Rectangle SetCellTextureSize()
        {
            var sizeAlive = new Vector2(Cell_Alive.Width, Cell_Alive.Height);
            var sizeDead = new Vector2(Cell_Dead.Width, Cell_Dead.Height);
            int x;
            int y;

            if (sizeAlive.X > sizeDead.X)
                x = (int)sizeAlive.X;
            else
                x = (int)sizeDead.X;

            if (sizeAlive.Y > sizeDead.Y)
                y = (int)sizeAlive.Y;
            else
                y = (int)sizeDead.Y;

            return new Rectangle(0,0,x,y);
        }


        public static Texture2D GetCellTexture(CellState state)
        {
            switch (state)
            {
                case CellState.Alive:
                    return Cell_Alive;
                case CellState.Dead:
                    return Cell_Dead;
                default:
                    return Cell_Dead;
            }
        }
    }
}
