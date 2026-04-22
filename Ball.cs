using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace breakout
{
    public class Ball
    {
        private Rectangle _location;
        private Vector2 _speed;
        private Texture2D _apearance;

        public Rectangle Rect
        {
            get { return _location; }
        }

        public Vector2 Speed
        {
            get { return _speed; }
        }

        public Ball(Texture2D appearance, Rectangle location)
        {
            _apearance = appearance;
            _location = location;
            _speed = new Vector2(3, -3);
        }
        public void Update(Rectangle window, Rectangle Paddle, float paddleSpeed)
        {
            _location.X += (int)_speed.X;
            _location.Y += (int)_speed.Y;
            
            if(_location.X <= 0)
            {
                _location.X = 0;
                _speed.X *= -1;
            }
            else if(_location.X + _location.Width >= window.Width)
            {
                _location.X = window.Width - _location.Width;
                _speed.X *= -1;
            }
            if (_location.Y <= 0)
            {
                _location.Y = 0;
                _speed.Y *= -1;
            }

            if (_location.Intersects(Paddle) && _speed.Y > 0)
            {
                if (_location.Bottom - Paddle.Y <= 10)
                {
                    _location.Y = Paddle.Y - _location.Height;
                    _speed.Y *= -1;
                    _speed.X += paddleSpeed * 0.35f;

                    if (_speed.X > 5) _speed.X = 5;
                    if (_speed.X < -5) _speed.X = -5;
                }

                else if (_location.X < Paddle.X)
                {
                    _location.X = Paddle.X - _location.Width;
                    _speed.X = -Math.Abs(_speed.X);
                }
                else if (_location.X + _location.Width > Paddle.X + Paddle.Width)
                {
                    _location.X = Paddle.X + Paddle.Width;
                    _speed.X = Math.Abs(_speed.X);
                }

            }


        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_apearance, _location, Color.White);
        }

    }
}
