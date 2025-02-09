using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nito.Collections;
using System;

namespace MonoSnake
{
    public class Food
    {
        private Vector2 _position = new Vector2(5, 6);
        public Texture2D _texture;

        public Food()
        {
        }

        Vector2 GenerateRandomPosition(Deque<Vector2> _snakeBody)
        {
            Vector2 _result = _position;
            while (Globals.ElementInDeque(_snakeBody, _result))
            {
                Random rand = new Random();
                _result.X = rand.Next(0, Globals.cellsWidth);
                _result.Y = rand.Next(0, Globals.cellsHeight);
            }
            return _result;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public void SetRandomPosition(Deque<Vector2> _snakeBody)
        {
            _position = GenerateRandomPosition(_snakeBody);
        }

        public void Load(ContentManager _contentManager)
        {
            _texture = _contentManager.Load<Texture2D>("images/food");
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, new Vector2(Globals.cellSize * _position.X + Globals.offset, Globals.cellSize * _position.Y + Globals.offset), Globals.lightColor);
            _spriteBatch.End();
        }
    }
}
