using System;
using System.Data.SqlTypes;
using System.Reflection;
using MonoBrain.GameEngineScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoBrain
{
    public class GameEngine : Game
    {
        private SpriteBatch _spriteBatch;
        
        FileLoader _fileLoader = new ();
        
        public GameEngine()
        {
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            //there's a small hole on the left side of the screen
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            
            _fileLoader.File_Loader(
                Assembly.GetExecutingAssembly(),
                Assembly.GetEntryAssembly()
                );

            foreach (var start in FileLoader.start_Methods)
            {
                start();
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            foreach (Body sprite in Body.Bodies)
            {
                Console.WriteLine($"Trying to load sprite : {sprite.Directory}");
                try
                {
                    sprite.Sprite = Content.Load<Texture2D>(sprite.Directory);
                }
                catch
                {
                    Console.WriteLine($"can't find {sprite.Directory}");
                }
            }
        }
        

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // delta position (but not really)
            // body.DPosition = body.Position - body.PrePosition;

            foreach (var body in Body.Bodies)
            {
                body.Position += body.Velocity;
            }
            
            foreach (var update in FileLoader.update_Methods)
            {
                update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);
            
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            foreach (var body in Body.Bodies)
            {
                _spriteBatch.Draw(
                    body.Sprite,
                    body.Position.ToVector2,
                    null,
                    Color.White,
                    (float)body.Rotation,
                    new Vector((double)body.Sprite.Width / 2, (double)body.Sprite.Height / 2).ToVector2,
                    new Vector(Math.Abs(body.Scale.X), Math.Abs(body.Scale.Y)).ToVector2,
                    body.Scale.X < 0 ? (body.Scale.Y < 0 ? SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically : SpriteEffects.FlipHorizontally) : body.Scale.Y < 0 ? SpriteEffects.FlipVertically : SpriteEffects.None,
                    0);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
