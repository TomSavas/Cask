using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Components;
using Core.Events;
using Core.Extensions;
using Core.GameObjects;
using Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Core
{
    public class GameScene : SceneDecorator
    {
        public GameScene(IScene scene) : base(scene)
        {
            var gameObject = new GameObject("Mario", new Collection<IComponent>(), new ComponentDependencyResolver());
            gameObject.AddComponent<Transform>(new Transform());
            gameObject.AddComponent<Text>(new Text("Terminus"));
            gameObject.AddComponent<Sprite>(new Sprite("MarioSprite", 1));
            var mouseInputHandler = new MouseInputHandler(typeof(Transform), typeof(Text));
            EventAggregator.SubsribeEvent(mouseInputHandler);
            mouseInputHandler.InputHandlers.Add(new MouseState(), new List<Action<IComponentDependencies, MouseState>>
            {
                (dependencies, mouseState) =>
                {
                    var camera = GameObjects.First(go => go.Name == "Camera").Components.Get<Camera>();
                    dependencies.Get<Transform>().Position = new Vector3(camera.ScreenPointToWorldPoint(mouseState.Position.ToVector2()), 0);
                    dependencies.Get<Text>().TextContent = "Current mouse pos in screen space \nx: " + mouseState.X + "\ny: " + mouseState.Y;
                    dependencies.Get<Text>().TextContent += "\nCurrent camera pos in world space \nx: " +
                                                            camera.Dependencies.Get<Transform>().Position.X + 
                                                            "\ny: " + camera.Dependencies.Get<Transform>().Position.Y +
                                                            "\nCurrent mouse pos in world space \nx: " +
                                                            camera.ScreenPointToWorldPoint(mouseState.Position.ToVector2()).X +
                                                            "\ny: " + camera.ScreenPointToWorldPoint(mouseState.Position.ToVector2()).Y;
                }
            });
            gameObject.AddComponent<MouseInputHandler>(mouseInputHandler);
            GameObjects.Add(gameObject);
            
            gameObject = new GameObject("InvertedMario", new Collection<IComponent>(), new ComponentDependencyResolver());
            var invertedMario = gameObject;
            gameObject.AddComponent<Transform>(new Transform(new Vector3(50, 200, 0), new Vector3(0.5f, 1f, 1f)));
            gameObject.AddComponent<Sprite>(new Sprite("InvertedMarioSprite"));

            var keyboardInputHandler = new KeyboardInputHandler(typeof(Transform));
            EventAggregator.SubsribeEvent(keyboardInputHandler);
            keyboardInputHandler.OnButtonDown(Keys.W,
                components => components.Get<Transform>().Position += new Vector3(0, -2, 0));
            
            keyboardInputHandler.OnButtonDown(Keys.S,
                components => components.Get<Transform>().Position += new Vector3(0, 2, 0));
            
            keyboardInputHandler.OnButtonDown(Keys.A,
                components => components.Get<Transform>().Position += new Vector3(-2, 0, 0));

            keyboardInputHandler.OnButtonDown(Keys.D,
                components => components.Get<Transform>().Position += new Vector3(2, 0, 0));

            keyboardInputHandler.OnButtonDown(Keys.Space,
                components => components.Get<Transform>().Rotation *= Matrix.CreateFromYawPitchRoll(0, 0, MathHelper.PiOver4/10));
            
            gameObject.AddComponent<KeyboardInputHandler>(keyboardInputHandler);
            gameObject.GetComponent<Transform>().Rotation = Matrix.CreateFromYawPitchRoll(0, 0, MathHelper.PiOver4);
            GameObjects.Add(gameObject);
            
            gameObject = new GameObject("RedMario", new List<IComponent>(), new ComponentDependencyResolver());
            gameObject.AddComponent<Transform>(new Transform(new Vector3(200, 0, 0)));
            //gameObject.AddComponent<Transform>(new Transform(new Vector3(100, 0, 0), new Vector3(1, 2, 1), Matrix.CreateFromYawPitchRoll(0, 0, -MathHelper.PiOver2)));gameObject.AddComponent<Transform>(new Transform_	Transform_	()S)_	Transform_<	AddComponent();addcompob
            gameObject.AddComponent<Sprite>(new Sprite("RedMarioSprite", 2));
            GameObjects.Add(gameObject);
            gameObject.GetComponent<Transform>().SetTransformParent(invertedMario.GetComponent<Transform>());
        }
    }
}