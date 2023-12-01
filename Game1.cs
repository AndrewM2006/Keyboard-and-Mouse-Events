using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Keyboard_and_Mouse_Events
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D pacRightTexture;
        Texture2D pacLeftTexture;
        Texture2D pacUpTexture;
        Texture2D pacDownTexture;
        Texture2D pacSleepTexture;
        Rectangle pacLocation;
        KeyboardState keyboardState;
        KeyboardState oldState;
        MouseState mouseState;
        SpriteFont info;
        string whatTexture;
        int pacSpeed, previousScrollValue;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pacLocation = new Rectangle(10, 10, 75, 75);
            pacSpeed = 3;
            mouseState = Mouse.GetState();
            previousScrollValue = mouseState.ScrollWheelValue;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pacRightTexture = Content.Load<Texture2D>("PacRight");
            pacLeftTexture = Content.Load<Texture2D>("PacLeft");
            pacDownTexture = Content.Load<Texture2D>("PacDown");
            pacUpTexture = Content.Load<Texture2D>("PacUp");
            pacSleepTexture = Content.Load<Texture2D>("PacSleep");
            info = Content.Load<SpriteFont>("Info");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            oldState = keyboardState;
            Random generator = new Random();
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if (keyboardState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                pacLocation.X = generator.Next(_graphics.PreferredBackBufferWidth - pacLocation.Width);
                pacLocation.Y = generator.Next(_graphics.PreferredBackBufferHeight - pacLocation.Height);
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                pacLocation.Y -= pacSpeed;
                whatTexture = "Up";
                if (pacLocation.Bottom < 0)
                {
                    pacLocation.Y = _graphics.PreferredBackBufferHeight;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                pacLocation.Y += pacSpeed;
                whatTexture = "Down";
                if (pacLocation.Top > _graphics.PreferredBackBufferHeight)
                {
                    pacLocation.Y = 0 - pacLocation.Height;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                pacLocation.X -= pacSpeed;
                whatTexture = "Left";
                if (pacLocation.Right < 0)
                {
                    pacLocation.X = _graphics.PreferredBackBufferWidth;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                pacLocation.X += pacSpeed;
                whatTexture = "Right";
                if (pacLocation.Left > _graphics.PreferredBackBufferWidth)
                {
                    pacLocation.X = 0 - pacLocation.Width;
                }
            }
            else
            {
                whatTexture = "Sleep";
            }
            if (mouseState.LeftButton == ButtonState.Pressed && mouseState.X > 0 && mouseState.X<_graphics.PreferredBackBufferWidth && mouseState.Y>0 && mouseState.Y<_graphics.PreferredBackBufferHeight)
            {
                pacLocation.X = mouseState.X - pacLocation.Width / 2;
                pacLocation.Y = mouseState.Y - pacLocation.Height / 2;
            }
            if (mouseState.ScrollWheelValue > previousScrollValue)
            {
                if (pacSpeed < 30)
                {
                    pacSpeed++;
                }
            }
            else if (mouseState.ScrollWheelValue < previousScrollValue)
            {
                if (pacSpeed > 1)
                {
                    pacSpeed--;
                }
            }
            previousScrollValue = mouseState.ScrollWheelValue;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawString(info, "Use The Scroll Wheel To Control Speed", new Vector2(20, 100), Color.White);
            switch (whatTexture)
            {
                case "Up":
                    _spriteBatch.Draw(pacUpTexture, pacLocation, Color.White);
                    break;
                case "Down":
                    _spriteBatch.Draw(pacDownTexture, pacLocation, Color.White);
                    break;
                case "Right":
                    _spriteBatch.Draw(pacRightTexture, pacLocation, Color.White);
                    break;
                case "Left":
                    _spriteBatch.Draw(pacLeftTexture, pacLocation, Color.White);
                    break;
                case "Sleep":
                    _spriteBatch.Draw(pacSleepTexture, pacLocation, Color.White);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}