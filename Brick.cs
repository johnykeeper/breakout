using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace breakout
{
    public class Brick
    {
        private Rectangle _location;
        private Texture2D _appearance;
        private Color _color;

        public Rectangle Rect
        {
            get { return _location; }
        }
        public Color Color
        {
            get { return _color; }
        }
        public Brick(Texture2D appearance, Rectangle location, Color color)
        {
            _appearance = appearance;
            _location = location;
            _color = color;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_appearance, _location, _color);
        }





    }
}
