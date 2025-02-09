using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Nito.Collections;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace MonoSnake
{
    public class GameManager
    {
        public int score = 0;
        SpriteFont game_over_font;
        Snake _snake = new Snake();
        Food _food = new Food();
        bool paused = false;
        SoundEffect eatSFX;
        SoundEffect dieSFX;

        public void Load(ContentManager _contentManager)
        {
            _snake.Load(_contentManager);
            _food.Load(_contentManager);
            _food.SetRandomPosition(_snake.body);
            game_over_font = _contentManager.Load<SpriteFont>("fonts/game_over_font");
            eatSFX = _contentManager.Load<SoundEffect>("audio/eat");
            dieSFX = _contentManager.Load<SoundEffect>("audio/wall");
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            if (!paused)
            {
                _snake.Draw(_spriteBatch);
                _food.Draw(_spriteBatch);
            } else
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(game_over_font, "Game Over", new Vector2((Globals.width / 2 - 300), (Globals.height / 2 - 100)), Globals.lightColor);
                _spriteBatch.End();
            }
        }

        public void Update(GameTime _gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) ||
                Keyboard.GetState().IsKeyDown(Keys.Up) ||
                Keyboard.GetState().IsKeyDown(Keys.Right) ||
                Keyboard.GetState().IsKeyDown(Keys.Left) ||
                Keyboard.GetState().IsKeyDown(Keys.Down) ||
                Keyboard.GetState().IsKeyDown(Keys.W) ||
                Keyboard.GetState().IsKeyDown(Keys.A) ||
                Keyboard.GetState().IsKeyDown(Keys.S) ||
                Keyboard.GetState().IsKeyDown(Keys.D))
            {
                paused = false;
            }
            if (!paused)
            {
                _snake.Update(_gameTime);
                CheckCollisionWithFood();
                CheckCollisionWithEdges();
                CheckCollisionWithTail();
            }
        }

        void CheckCollisionWithFood()
        {
            if (_snake.body[0] == _food.GetPosition())
            {
                eatSFX.Play();
                score++;
                _food.SetRandomPosition(_snake.body);
                _snake.Grow();
            }
        }

        void CheckCollisionWithEdges()
        {
            if (_snake.body[0].X == -1 || _snake.body[0].X == Globals.cellsWidth)
            {
                GameOver();
            }
            if (_snake.body[0].Y == -1 || _snake.body[0].Y == Globals.cellsHeight)
            {
                GameOver();
            }
        }

        void CheckCollisionWithTail()
        {
            Deque<Vector2> _headlessBody = new Deque<Vector2>();
            for (int i = 1; i < _snake.body.Count; i++)
            {
                _headlessBody.AddToBack(_snake.body[i]);
            }

            _headlessBody.RemoveFromFront();
            for (int i = 0; i < _headlessBody.Count; i++)
            {
                if (Globals.ElementInDeque(_headlessBody, _snake.body[0]))
                {
                    GameOver();
                }
            }
        }
        void GameOver()
        {
            score = 0;
            dieSFX.Play();
            _snake.Reset();
            _food.SetRandomPosition(_snake.body);
            paused = true;
        }
    }
}
