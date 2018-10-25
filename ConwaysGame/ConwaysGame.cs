using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ConwaysGame.Library;
using System.Collections.Generic;

namespace ConwaysGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ConwaysGame : Game
    {
        #region [ Members ]
        private GraphicsDeviceManager _graphics;
        public SpriteBatch GlobalSpriteBatch { get; private set; }

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        int _screenWidth;
        int _screenHeight;
        bool _screenSizeChanged;


        // Specific Junk
        Texture2D background { get; set; }
        CellGrid CellGrid { get; set; }

        #endregion


        #region [ Constructor ]
        public ConwaysGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        #endregion


        #region [ Initialize ]
        protected override void Initialize()
        {
            ScreenWidth = 800;
            ScreenHeight = 600;

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            Window.IsBorderless = false;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            Window.Title = "Conway's Game Of Life";

            base.Initialize();
        }
        #endregion


        #region [ Event: Window_ClientSizeChanged ]
        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _screenSizeChanged = true;
            _screenWidth = Window.ClientBounds.Width;
            _screenHeight = Window.ClientBounds.Height;
        }

        #endregion


        #region [ LoadContent ]
        protected override void LoadContent()
        {
            GlobalSpriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.LoadContent(Content);

            Mouse.SetCursor(MouseCursor.FromTexture2D(Assets.MouseCursor, 0, 0));
            background = Assets.Background;

            var cellGridPosition = new Vector2(50, 50);
            CellGrid = new CellGrid(
                new Size(Assets.Cell_Alive.Height, Assets.Cell_Alive.Width),
                cellGridPosition,
                22,
                16);
            CellGrid.ApplyLifePattern(PatternLibrary.Toad);
            CellGrid.SetInterval(1);
        }
        #endregion


        #region [ UnloadContent ]
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion


        #region [ Update ]
        protected override void Update(GameTime gameTime)
        {
            // SCREEN SIZE CHANGES
            if (_screenSizeChanged)
            {
                _graphics.PreferredBackBufferWidth = _screenWidth;
                _graphics.PreferredBackBufferHeight = _screenHeight;
                _graphics.ApplyChanges();
                _screenSizeChanged = false;

                ScreenWidth = _screenWidth;
                ScreenHeight = _screenHeight;

            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
                

            // TODO: Add your update logic here
            CellGrid.Update(gameTime);
            base.Update(gameTime);
        }
        #endregion


        #region [ Draw ]
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32,32,32));

            // TODO: Add your drawing code here
            GlobalSpriteBatch.Begin();
            // Background:
            // GlobalSpriteBatch.Draw(background, Vector2.Zero, Color.White);

            // Testing Stuff
            DrawGrid(GlobalSpriteBatch);

            // Mouse Cursor:  Last to draw in GlobalSpriteBatch
            // to ensure it is on top of z-position.
            DrawMouseCursor(GlobalSpriteBatch);


            // Last: SpriteBatch End and Call Base
            GlobalSpriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion


        #region [ DrawMouseCursor ]
        public void DrawMouseCursor(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assets.MouseCursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
        }
        #endregion


        #region [ DrawGrid ]
        public void DrawGrid(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < CellGrid.Grid.Count; i++)
            {
                var texture = Assets.GetCellTexture(CellGrid.Grid[i].State);
                spriteBatch.Draw(texture, CellGrid.Grid[i].TexturePosition, Color.White);
            }
        }
        #endregion
    }
}
