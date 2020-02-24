using System;
using System.Linq;
using Cask.Components;
using Cask.Managers;
using Cask.Scenes;
using Core.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cask
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private SpriteFont font;
		
		private ISceneManager _sceneManager;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			IsMouseVisible = true;
			TargetElapsedTime = TimeSpan.FromTicks(166666);
			Console.WriteLine("Initializing...");
			
			var basicGameElementFactory = new BasicGameElementFactory();
			var gameScene = new GameScene(basicGameElementFactory.MakeScene(Content, graphics));
			gameScene.GameManagers.Add(typeof(KeyboardInputManager), new KeyboardInputManager(gameScene.EventAggregator));
			gameScene.GameManagers.Add(typeof(MouseInputManager), new MouseInputManager(gameScene.EventAggregator));
			
            var keyboardInputHandler = new KeyboardInputHandler(basicGameElementFactory.MakeComponent());
			
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.Up), 
				(keyboardState, components) => components.GetComponent<Transform>().Position += new Vector3(0, -2, 0));
            
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.Down), 
				(keyboardState, components) => components.GetComponent<Transform>().Position += new Vector3(0, 2, 0));
			
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.Left), 
				(keyboardState, components) => components.GetComponent<Transform>().Position += new Vector3(-2, 0, 0));
			
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.Right), 
				(keyboardState, components) => components.GetComponent<Transform>().Position += new Vector3(2, 0, 0));
			
            gameScene.EventAggregator.SubsribeEvent(keyboardInputHandler);
			gameScene.GameObjects.First(go => go.Name == "Camera")
				.AddComponent<KeyboardInputHandler>(keyboardInputHandler);
			
			_sceneManager = new SceneManager(gameScene, Content);
			
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = Content.Load<SpriteFont>("Terminus");

			_sceneManager.LoadScene("MainScene", false);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			update++;
			_sceneManager.Update(gameTime);
			
			base.Update(gameTime);
		}

		private int update = 0, draw = 0;

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			draw++;
			_sceneManager.Draw(gameTime);
			
			spriteBatch.Begin();
			spriteBatch.DrawString(font,
                    $"SPF: {gameTime.ElapsedGameTime.Milliseconds} ms\nDraw call count: {draw}\nUpdate call count: {update}",
                    Vector2.Zero,
                    Color.White,
                    0,
                    Vector2.Zero, 
                    1, 
                    SpriteEffects.None, 
                    0);
			spriteBatch.End();
			base.Draw(gameTime);
			
			//GraphicsDevice.Clear(Color.Fuchsia);
			//base.Draw(gameTime);
		}
	}
}
