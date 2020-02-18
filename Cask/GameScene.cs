using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            IGameObject gameObject = new BasicGameObject("Mario", new Collection<IComponent> {new Transform(gameElementFactory.MakeComponent()), new Text(gameElementFactory.MakeComponent(), "Terminus"), new Sprite(gameElementFactory.MakeComponent(), "MarioSprite", 1)}, new ComponentDependencyResolver());
            var mouseInputHandler = new MouseInputHandler(gameElementFactory.MakeComponent(), typeof(Transform), typeof(Text));
            EventAggregator.SubsribeEvent(mouseInputHandler);
	        var mouseWhatever = new MouseState().ScrollWheelValue;
            mouseInputHandler.AddEventListener(mouseState => true, (mouseState, dependencies) =>
                {
                    var camera = GameObjects.First(go => go.Name == "Camera").GetComponent<Camera>();
	                dependencies.Get<Transform>().Position = new Vector3(camera.ScreenToWorld(mouseState.Position.ToVector2()), 0);
                    dependencies.Get<Text>().TextContent = "Current mouse pos in screen space \nx: " + mouseState.X + "\ny: " + mouseState.Y;
                    dependencies.Get<Text>().TextContent += "\nCurrent camera pos in world space \nx: " +
                                                            camera.Dependencies.Get<Transform>().Position.X + 
                                                            "\ny: " + camera.Dependencies.Get<Transform>().Position.Y +
                                                            "\nCurrent mouse pos in world space \nx: " +
                                                            dependencies.Get<Transform>().Position.X +
                                                            "\ny: " + dependencies.Get<Transform>().Position.Y;

	                var scale = (mouseWhatever - mouseState.ScrollWheelValue) * 0.001f;
	                if (Math.Abs(scale) > 0.001f)
	                {
		                mouseWhatever = mouseState.ScrollWheelValue;
	                	camera.Dependencies.Get<Transform>().Scale += new Vector3(scale, scale, 0);
	                }
                }
            );
            gameObject.AddComponent<MouseInputHandler>(mouseInputHandler);
            GameObjects.Add(gameObject);
            
            gameObject = new BasicGameObject("InvertedMario", new Collection<IComponent>(), new ComponentDependencyResolver());
            var invertedMario = gameObject;
            gameObject.AddComponent<Transform>(new Transform(gameElementFactory.MakeComponent(), new Vector3(50, 200, 0), new Vector3(0.5f, 1f, 1f)));
            gameObject.AddComponent<Sprite>(new Sprite(gameElementFactory.MakeComponent(), "InvertedMarioSprite"));

            var keyboardInputHandler = new KeyboardInputHandler(gameElementFactory.MakeComponent(), typeof(Transform));
            EventAggregator.SubsribeEvent(keyboardInputHandler);
            keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.W), 
				(keyboardState, components) => components.Get<Transform>().Position += new Vector3(0, -2, 0));
            
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.S), 
				(keyboardState, components) => components.Get<Transform>().Position += new Vector3(0, 2, 0));
			
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.A), 
				(keyboardState, components) => components.Get<Transform>().Position += new Vector3(-2, 0, 0));
			
			keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.D), 
				(keyboardState, components) => components.Get<Transform>().Position += new Vector3(2, 0, 0));

	        keyboardInputHandler.AddEventListener(
				keyboardState => keyboardState.IsKeyDown(Keys.Space), 
				(keyboardState, components) => components.Get<Transform>().Rotation *= Matrix.CreateFromYawPitchRoll(0, 0, MathHelper.PiOver4/10));

            gameObject.AddComponent<KeyboardInputHandler>(keyboardInputHandler);
            gameObject.GetComponent<Transform>().Rotation = Matrix.CreateFromYawPitchRoll(0, 0, MathHelper.PiOver4);
            GameObjects.Add(gameObject);
            
            gameObject = new BasicGameObject("RedMario", new List<IComponent>(), new ComponentDependencyResolver());
            gameObject.AddComponent<Transform>(new Transform(gameElementFactory.MakeComponent(), new Vector3(200, 0, 0)));
            //gameObject.AddComponent<Transform>(new Transform(new Vector3(100, 0, 0), new Vector3(1, 2, 1), Matrix.CreateFromYawPitchRoll(0, 0, -MathHelper.PiOver2)));gameObject.AddComponent<Transform>(new Transform_	Transform_	()S)_	Transform_<	AddComponent();addcompob
            gameObject.AddComponent<Sprite>(new Sprite(gameElementFactory.MakeComponent(), "RedMarioSprite", 2));
            GameObjects.Add(gameObject);
            gameObject.GetComponent<Transform>().SetTransformParent(invertedMario.GetComponent<Transform>());
	        
            /*
	        gameObject = new BasicGameObject("Map", new Collection<IComponent>(), new ComponentDependencyResolver());
	        gameObject.AddComponent<Transform>(new Transform(gameElementFactory.MakeComponent()));
	        gameObject.AddComponent<Map>(new Map(gameElementFactory.MakeComponent(), "island"));
	        GameObjects.Add(gameObject);
	        */
        }
    }
}