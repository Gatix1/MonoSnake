using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoSnake
{
    public class MonoSnake : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Texture2D line_segment_texture;
        SpriteFont _font;

        GameManager _gameManager = new GameManager();
        public MonoSnake()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Globals.cellSize * Globals.cellsWidth + Globals.offset * 2;
            _graphics.PreferredBackBufferHeight = Globals.cellSize * Globals.cellsHeight + Globals.offset * 2;
            _graphics.ApplyChanges();
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 160d);
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameManager.Load(Content);
            line_segment_texture = Content.Load<Texture2D>("images/line_sample");
            _font = Content.Load<SpriteFont>("fonts/m5x7");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _gameManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Globals.darkColor);

            // Drawing outline
            _spriteBatch.Begin();
            _spriteBatch.Draw(line_segment_texture, new Rectangle(Globals.offset - 5, Globals.offset - 5, Globals.cellSize * Globals.cellsWidth + 10, 5), Globals.lightColor);
            _spriteBatch.Draw(line_segment_texture, new Rectangle(Globals.offset - 5, Globals.cellSize * Globals.cellsHeight + Globals.offset, Globals.cellSize * Globals.cellsWidth + 10, 5), Globals.lightColor);
            _spriteBatch.Draw(line_segment_texture, new Rectangle(Globals.offset - 5, Globals.offset - 5, 5, Globals.cellSize * Globals.cellsHeight + 10), Globals.lightColor);
            _spriteBatch.Draw(line_segment_texture, new Rectangle(Globals.offset + Globals.cellSize * Globals.cellsWidth, Globals.offset - 5, 5, Globals.cellSize * Globals.cellsHeight + 10), Globals.lightColor);
            _spriteBatch.DrawString(_font, "MonoSnake", new Vector2(Globals.offset, Globals.offset - 45), Globals.lightColor);
            _spriteBatch.DrawString(_font, "Score: " + _gameManager.score.ToString(), new Vector2(Globals.offset, Globals.cellSize * Globals.cellsHeight + Globals.offset), Globals.lightColor);
            _spriteBatch.End();

            _gameManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
