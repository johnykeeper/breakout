using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        public void Update(Rectangle window, Rectangle Paddle, float paddleSpeed, List<Brick> bricks)
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
            else if(_location.Y + _location.Height >= window.Height)
            {
                _location.Y = window.Height - _location.Height;
                _speed.Y *= -1;
            }

            if (_location.Intersects(Paddle) && _speed.Y > 0)
            {
                if (_location.Bottom - Paddle.Y <= 10)
                {
                    _location.Y = Paddle.Y - _location.Height;
                    _speed.Y *= -1;
                    _speed.X += paddleSpeed * 0.35f;

                    if (_speed.X > 6) _speed.X = 6;
                    if (_speed.X < -6) _speed.X = -6;
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
            Brick hitBrick = null;

            foreach (Brick b in bricks)
            {
                if (_location.Intersects(b.Rect))
                {
                    hitBrick = b;



                    if (_location.Bottom - b.Rect.Top <= 10)
                    {
                        _location.Y = b.Rect.Top - _location.Height;
                        _speed.Y *= -1;
                    }
                    else if (b.Rect.Bottom - _location.Top <= 10)
                    {
                        _location.Y = b.Rect.Bottom;
                        _speed.Y *= -1;
                    }
                    else if (_location.X < b.Rect.X)
                    {
                        _location.X = b.Rect.X - _location.Width;
                        _speed.X = -Math.Abs(_speed.X);
                    }
                    else
                    {
                        _location.X = b.Rect.X + b.Rect.Width;
                        _speed.X = Math.Abs(_speed.X);
                    }

                    break;
                }
            }
            if (hitBrick != null) 
                bricks.Remove(hitBrick);

        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_apearance, _location, Color.White);
        }

    }
}
