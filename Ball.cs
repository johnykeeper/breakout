using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public void Update()
        {

        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_apearance, _location, Color.White);
        }

    }
}
