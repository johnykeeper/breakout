using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
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
        List<Particle> particles;
        Texture2D paddleTexture, ballTexture, brickTexture, backgroundTexture, breakoutLogo, breakoutLose, breakoutWin;
        KeyboardState keyboardState;
        Rectangle window;
        SpriteFont font;
        Texture2D heartTexture;
        int lives;
        enum Screen { Title, Game, End, Win }
        Screen screen;
        SoundEffect brickHitSound;
        Song backgroundMusic;

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
            backgroundTexture = Content.Load<Texture2D>("coolspace");
            paddle = new Paddle(paddleTexture, new Rectangle(350, 550, 100, 20), window);
            font = Content.Load<SpriteFont>("font");
            screen = Screen.Title;
            breakoutLogo = Content.Load<Texture2D>("Breakout_OG-logo");
            breakoutLose = Content.Load<Texture2D>("breakout-lose");
            breakoutWin = Content.Load<Texture2D>("breakout-win");
            brickHitSound = Content.Load<SoundEffect>("firstpop");
            backgroundMusic = Content.Load<Song>("music");
            heartTexture = Content.Load<Texture2D>("heartBreakout");
            lives = 3;

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.Volume = 0.15f;

            ball = new Ball(ballTexture, new Rectangle(390, 530, 20, 20));
            Color[] rowColors = {Color.DarkRed, Color.DarkOrange, Color.Goldenrod, Color.OliveDrab, Color.DarkSlateBlue};

            bricks = new List<Brick>();
            particles = new List<Particle>();

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    bricks.Add(new Brick(brickTexture, new Rectangle(col * 78 + 10, row * 30 + 50, 70, 25), rowColors[row]));


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

           
            base.Update(gameTime);
            particles.RemoveAll(p => p.IsDead);
            foreach (Particle p in particles)
                p.Update();
            //screen stuff
            if(screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                    screen = Screen.Game;
            }
            else if(screen == Screen.Game)
            {
                paddle.Update(keyboardState);
                ball.Update(window, paddle.Rect, paddle.SpeedX, bricks, particles, brickHitSound);
                if (bricks.Count == 0)
                    screen = Screen.Win;
                if (ball.Rect.Y > window.Height)
                {
                    lives--;
                    if (lives <= 0)
                        screen = Screen.End;

                    else
                    {
                        ball = new Ball(ballTexture, new Rectangle(390, 530, 20, 20));
                        paddle = new Paddle(paddleTexture, new Rectangle(350, 550, 100, 20), window);
                    }
                }
            }
            else if(screen == Screen.End)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                    ResetGame();
            }
            else if(screen == Screen.Win)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                    ResetGame();
            }
            
        }
        private void ResetGame()
        {
            screen = Screen.Title;
            lives = 3;
            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                    screen = Screen.Game;
            }
            ball = new Ball(ballTexture, new Rectangle(390, 530, 20, 20));
            paddle = new Paddle(paddleTexture, new Rectangle(350, 550, 100, 20), window);
            particles.Clear();
            bricks.Clear();
            Color[] rowColors = { Color.DarkRed, Color.DarkOrange, Color.Goldenrod, Color.OliveDrab, Color.DarkSlateBlue };
            for (int row = 0; row < 5; row++)
                for (int col = 0; col < 10; col++)
                    bricks.Add(new Brick(brickTexture, new Rectangle(col * 78 + 10, row * 30 + 50, 70, 25), rowColors[row]));

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if(screen == Screen.Title)
            {
                _spriteBatch.Draw(breakoutLogo, window, Color.White);
                _spriteBatch.DrawString(font, "Press Enter to play", new Vector2(300, 500), Color.White);
            }
            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(backgroundTexture, window, Color.White);
                paddle.Draw(_spriteBatch);
                ball.Draw(_spriteBatch);
                foreach (Brick b in bricks)
                    b.Draw(_spriteBatch);
                foreach (Particle p in particles)
                    p.Draw(_spriteBatch, brickTexture);
                for (int i = 0; i < lives; i++)
                {
                    _spriteBatch.Draw(heartTexture, new Rectangle(10 + i * 40, 10, 30, 30), Color.White);
                }
            }
            else if(screen == Screen.End)
            {
                _spriteBatch.Draw(breakoutLose, window, Color.White);
                
            }
            else if(screen == Screen.Win)
            {
                _spriteBatch.Draw(breakoutWin, window, Color.White);
            }
                _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
