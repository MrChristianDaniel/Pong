using System;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Project_3
{
    public class Game3 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ballSprite;
        Texture2D paddleSprite;
        SpriteFont spriteFont;

        const int BALL_RADIUS = 5;
        const int PADDLE_RADIUS = 25;
        int score;

        Vector2 ballPosition;
        Vector2 ballSpeed;
        MouseState mState;

        public Game3()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            ballPosition = new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2);
            ballSpeed = new Vector2(1, 1);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ballSprite = Content.Load<Texture2D>("particle");
            paddleSprite = Content.Load<Texture2D>("paddle");
            spriteFont = Content.Load<SpriteFont>("File");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            mState = Mouse.GetState();

            Rectangle ballRectangle = new Rectangle((int)ballPosition.X-BALL_RADIUS, (int)ballPosition.Y-BALL_RADIUS, ballSprite.Width, ballSprite.Height);
            Rectangle paddleRectangle = new Rectangle((int)mState.X-PADDLE_RADIUS, GraphicsDevice.Viewport.Height-50, paddleSprite.Width, paddleSprite.Height);

            if (ballPosition.X >= GraphicsDevice.Viewport.Width - BALL_RADIUS || ballPosition.X <= BALL_RADIUS)
            {
                ballSpeed.X = -ballSpeed.X;
            }
            if (ballPosition.Y <= BALL_RADIUS)
            {
                ballSpeed.Y = -ballSpeed.Y;
            }
            if (ballRectangle.Intersects(paddleRectangle))
            {
                ballSpeed.Y = -ballSpeed.Y;
                score++;
                if (ballSpeed.X < 0)
                {
                    ballSpeed.X += 1;
                }
                else
                {
                    ballSpeed.X += 1;
                }
                if (ballSpeed.Y < 0)
                {
                    ballSpeed.Y += -1;
                }
                else
                {
                    ballSpeed.Y += 1;
                }
            }
            if (ballPosition.Y >= GraphicsDevice.Viewport.Height)
            {
                Exit();
            }
            ballPosition.X += ballSpeed.X;
            ballPosition.Y += ballSpeed.Y;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.Draw(ballSprite, new Vector2(ballPosition.X - BALL_RADIUS, ballPosition.Y - BALL_RADIUS), Color.White);
            spriteBatch.Draw(paddleSprite, new Vector2(mState.X-PADDLE_RADIUS, GraphicsDevice.Viewport.Height - 50), Color.White);
            spriteBatch.DrawString(spriteFont, "Score: " + score.ToString(), new Vector2(5, 5), Color.White);

            spriteBatch.End();
        }
    }
}