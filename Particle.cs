using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace breakout
{
    internal class Particle
    {
        private Rectangle _location;
        private Vector2 _speed;
        private float _life;
        private Color _color;
        
        public bool IsDead {  get { return _life <= 0; } }

        public Particle(Rectangle location, Vector2 speed, Color color)
        {
            _location = location;
            _speed = speed;
            _color = color;
            _life = 1f;
        }

        public void Update()
        {
            _location.X += (int)_speed.X;
            _location.Y += (int)_speed.Y;
            _life -= 0.05f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, _location, _color * _life);
        }
    }
}
