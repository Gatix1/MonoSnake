using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nito.Collections;

namespace MonoSnake
{
    public class Snake
    {
        double lastUpdateTime = 0;
        double updateInterval = 130;
        bool _isGrowing = false;
        bool intervalPassed(GameTime gameTime, double interval)
        {
            double currentTime = gameTime.TotalGameTime.TotalMilliseconds;
            if (currentTime - lastUpdateTime >= interval)
            {
                lastUpdateTime = currentTime;
                return true;
            }
            return false;
        }

        Texture2D _texture;
        Vector2 _direction = new Vector2(1, 0);
        Vector2 _lastDirection = new Vector2(0, 0);

        public Deque<Vector2> body = new Deque<Vector2>();

        public Snake()
        {
            body.AddToBack(new Vector2(6, 9));
            body.AddToBack(new Vector2(5, 9));
            body.AddToBack(new Vector2(4, 9));
        }

        public void Load(ContentManager _contentManager)
        {
            _texture = _contentManager.Load<Texture2D>("images/snake_tile");
        }

        public void Update(GameTime gameTime)
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.Up) ||
                Keyboard.GetState().IsKeyDown(Keys.W)) &&
                _direction.Y != 1 &&
                _lastDirection.Y != 1)
                _direction = new Vector2(0, -1);
            if ((Keyboard.GetState().IsKeyDown(Keys.Down) ||
                Keyboard.GetState().IsKeyDown(Keys.S)) &&
                _direction.Y != -1 &&
                _lastDirection.Y != -1)
                _direction = new Vector2(0, 1);
            if ((Keyboard.GetState().IsKeyDown(Keys.Right) ||
                Keyboard.GetState().IsKeyDown(Keys.D)) &&
                _direction.X != -1 &&
                _lastDirection.X != -1)
                _direction = new Vector2(1, 0);
            if ((Keyboard.GetState().IsKeyDown(Keys.Left) ||
                Keyboard.GetState().IsKeyDown(Keys.A)) &&
                _direction.X != 1 && 
                _lastDirection.X != 1)
                _direction = new Vector2(-1, 0);

            if (intervalPassed(gameTime, updateInterval))
                HandleMovement();
        }

        public void Reset()
        {
            body.Clear();
            body.AddToBack(new Vector2(6, 9));
            body.AddToBack(new Vector2(5, 9));
            body.AddToBack(new Vector2(4, 9));
            _direction = new Vector2(1, 0);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            for(int i = 0; i < body.Count; i++)
            {
                int x = (int)body[i].X * Globals.cellSize;
                int y = (int)body[i].Y * Globals.cellSize;
                _spriteBatch.Draw(_texture, new Vector2(x + Globals.offset, y + Globals.offset), Globals.lightColor);
            }
            _spriteBatch.End();
        }

        void HandleMovement()
        {
            if (_isGrowing)
            {
                body.AddToBack(body[0] + _direction);
                _isGrowing = false;
            }
            body.RemoveFromBack();
            body.AddToFront(body[0] + _direction);
            _lastDirection = _direction;
        }

        public void Grow()
        {
            _isGrowing = true;
        }
    }
}
