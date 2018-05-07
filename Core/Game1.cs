using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Events;
using Core.Factories;
using Core.GameObjects;
using Core.Managers;
using Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private Texture2D texture;
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
			
			var basicGameElementFactory = new BasicGameElementFactory();
			var gameScene = new GameScene(basicGameElementFactory.MakeScene(Content, graphics));
			gameScene.GameManagers.Add(typeof(KeyboardInputManager), new KeyboardInputManager(gameScene.EventAggregator));
			gameScene.GameManagers.Add(typeof(MouseInputManager), new MouseInputManager(gameScene.EventAggregator));
			
            var keyboardInputHandler = new KeyboardInputHandler(typeof(Transform));
			keyboardInputHandler.OnButtonDown(Keys.Up,
                components => components.Get<Transform>().Position += new Vector3(0, -2, 0));
            
            keyboardInputHandler.OnButtonDown(Keys.Down,
                components => components.Get<Transform>().Position += new Vector3(0, 2, 0));
            
            keyboardInputHandler.OnButtonDown(Keys.Left,
                components => components.Get<Transform>().Position += new Vector3(-2, 0, 0));

            keyboardInputHandler.OnButtonDown(Keys.Right,
                components => components.Get<Transform>().Position += new Vector3(2, 0, 0));
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
			texture = Content.Load<Texture2D>("MarioSprite");
			font = Content.Load<SpriteFont>("Terminus");

			_sceneManager.LoadScene("MainScene");
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			_sceneManager.Update(gameTime);
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			_sceneManager.Draw(gameTime);
			
			spriteBatch.Begin();
			spriteBatch.DrawString(font,
                    $"FPS: {(1 / gameTime.ElapsedGameTime.TotalSeconds).ToString("0.0")}",
                    Vector2.Zero,
                    Color.White,
                    0,
                    Vector2.Zero, 
                    1, 
                    SpriteEffects.None, 
                    0);
			//spriteBatch.DrawString(font,  $"FPS: {(1 / gameTime.ElapsedGameTime.TotalSeconds).ToString("0.0")}", Vector2.Zero, Color.White);	
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}