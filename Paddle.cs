using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace breakout
{
    public class Paddle
    {
        private Texture2D _texture;
        private Rectangle _rect;
        private Rectangle _window;
        private Vector2 _speed;
        public Rectangle Rect
        {
            get { return _rect; }
        }
        public Paddle(Texture2D texture, Rectangle rect, Rectangle window)
        {
            _texture = texture;
            _rect = rect;
            _window = window;
            _speed = Vector2.Zero;
        }
        public void update(KeyboardState keyboardState)
        {
            _speed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Left))
                _speed.X = -5;
            else if(keyboardState.IsKeyDown(Keys.Right))
                _speed.X = 5;
            _rect.X += (int)_speed.X;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_texture, _rect, Color.White);

        }


    }
}
