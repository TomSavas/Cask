using System;
using System.Linq;
using Cask.Components;
using Cask.GameObjects;
using Cask.Scenes;
using Core.Factories;
using Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cask
{
    public class GameScene : SceneDecorator
    {
        public GameScene(IScene scene) : base(scene)
        {
            var gameElementFactory = new BasicGameElementFactory();
            IGameObject gameObject = new BasicGameObject("Mario")
	            .AddComponent<Transform>(new Transform(gameElementFactory.MakeComponent()))
	            .AddComponent<Text>(new Text(gameElementFactory.MakeComponent(), "Terminus"))
	            .AddComponent<Sprite>(new Sprite(gameElementFactory.MakeComponent(), "MarioSprite", 1)) as IGameObject;
            var mouseInputHandler = new MouseInputHandler(gameElementFactory.MakeComponent());
            EventAggregator.SubsribeEvent(mouseInputHandler);
	        var mouseWhatever = new MouseState().ScrollWheelValue;
            mouseInputHandler.AddEventListener(mouseState => true, (mouseState, dependencies) =>
                {
                    var camera = GameObjects.First(go => go.Name == "Camera").GetComponent<Camera>();
	                dependencies.GetComponent<Transform>().Position = new Vector3(camera.ScreenToWorld(mouseState.Position.ToVector2()), 0);
                    dependencies.GetComponent<Text>().TextContent = "Current mouse pos in screen space \nx: " + mouseState.X + "\ny: " + mouseState.Y;
                    dependencies.GetComponent<Text>().TextContent += "\nCurrent camera pos in world space \nx: " +
                                                            camera.Dependencies.GetComponent<Transform>().Position.X + 
                                                            "\ny: " + camera.Dependencies.GetComponent<Transform>().Position.Y +
                                                            "\nCurrent mouse pos in world space \nx: " +
                                                            dependencies.GetComponent<Transform>().Position.X +
                                                            "\ny: " + dependencies.GetComponent<Transform>().Position.Y;

	                var scale = (mouseWhatever - mouseState.ScrollWheelValue) * 0.001f;
	                if (Math.Abs(scale) > 0.001f)
	                {
		                mouseWhatever = mouseState.ScrollWheelValue;
	                	camera.Dependencies.GetComponent<Transform>().Scale += new Vector3(scale, scale, 0);
	                }
                }
            );
            gameObject.AddComponent<MouseInputHandler>(mouseInputHandler);
            GameObjects.Add(gameObject);
            
            gameObject = new BasicGameObject("InvertedMario")
				.AddComponent<Transform>(new Transform(gameElementFactory.MakeComponent(), new Vector3(50, 200, 0), new Vector3(0.5f, 1f, 1f)))
				.AddComponent<Sprite>(new Sprite(gameElementFactory.MakeComponent(), "InvertedMarioSprite")) as IGameObject;

            var keyboardInputHandler = new KeyboardInputHandler(gameElementFactory.MakeComponent());
            EventAggregator.SubsribeEvent(keyboardInputHandler);
            keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.W), 
				(keyboardState, components) => components.GetComponent<Transform>().Position += new Vector3(0, -2, 0));
            
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.S), 
				(keyboardState, components) => components.GetComponent<Transform>().Position += new Vector3(0, 2, 0));
			
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.A), 
				(keyboardState, components) => components.GetComponent<Transform>().Position += new Vector3(-2, 0, 0));
			
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.D), 
				(keyboardState, components) => components.GetComponent<Transform>().Position += new Vector3(2, 0, 0));

	        keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.Space), 
				(keyboardState, components) => components.GetComponent<Transform>().Rotation *= Matrix.CreateFromYawPitchRoll(0, 0, MathHelper.PiOver4/10));

            gameObject.AddComponent<KeyboardInputHandler>(keyboardInputHandler);
            gameObject.GetComponent<Transform>().Rotation = Matrix.CreateFromYawPitchRoll(0, 0, MathHelper.PiOver4);
            GameObjects.Add(gameObject);
            var invertedMario = gameObject;
            
            gameObject = new BasicGameObject("RedMario")
	            .AddComponent<Transform>(new Transform(gameElementFactory.MakeComponent(), new Vector3(200, 0, 0)))
				.AddComponent<Sprite>(new Sprite(gameElementFactory.MakeComponent(), "RedMarioSprite", 2)) as IGameObject;
            //gameObject.AddComponent<Transform>(new Transform(new Vector3(100, 0, 0), new Vector3(1, 2, 1), Matrix.CreateFromYawPitchRoll(0, 0, -MathHelper.PiOver2)));gameObject.AddComponent<Transform>(new Transform_	Transform_	()S)_	Transform_<	AddComponent();addcompob
            GameObjects.Add(gameObject);
            gameObject.GetComponent<Transform>().SetTransformParent(invertedMario.GetComponent<Transform>());
        }
    }
}