using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Paddle paddle;
        Ball ball;
        List<Brick> bricks;
        Texture2D paddleTexture, ballTexture, brickTexture;
        KeyboardState keyboardState;
        Rectangle window;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            window = new Rectangle(0, 0, 800, 600);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            paddleTexture = Content.Load<Texture2D>("paddle");
            ballTexture = Content.Load<Texture2D>("circle");
            brickTexture = Content.Load<Texture2D>("rectangle");
            
            paddle = new Paddle(paddleTexture, new Rectangle(350, 550, 100, 20), window);

            ball = new Ball(ballTexture, new Rectangle(390, 530, 20, 20));


            bricks = new List<Brick>();


            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    bricks.Add(new Brick(brickTexture, new Rectangle(col * 78 + 10, row * 30 + 50, 70, 25), Color.White));


                }



            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            paddle.update(keyboardState);
            ball.Update(window, paddle.Rect, paddle.SpeedX);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            paddle.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);
            foreach (Brick b in bricks)
                b.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
