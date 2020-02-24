using System;
using System.Collections.Generic;
using Cask.Components;
using Cask.Events;
using Cask.Factories;
using Cask.GameObjects;
using Cask.Managers;
using Cask.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.ViewportAdapters;

namespace Core.Factories
{
    public class BasicGameElementFactory : IGameElementFactory
    {
        public IComponent MakeComponent()
        {
            return new BasicComponent();
        }

        public IGameObject MakeGameObject(string name = "GameObject")
        {
            return new BasicGameObject(name);
        }
        
        public IGameObject MakeCamera(GraphicsDeviceManager graphicsDeviceManager)
        {
            var gameObject = MakeGameObject("Camera");
            gameObject.AddComponent<Transform>(new Transform(MakeComponent()));
            gameObject.AddComponent<Camera>(new Camera(MakeComponent(), new DefaultViewportAdapter(graphicsDeviceManager.GraphicsDevice)));

            return gameObject;
        }
        
        public IScene MakeScene(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager)
        {
            var camera = MakeCamera(graphicsDeviceManager);
            var drawingManager = new DrawingManager(camera.GetComponent<Camera>());
            
            return new BasicScene(new List<IGameObject> {camera},
                new Dictionary<Type, IManager> {{typeof(IDrawingManager), drawingManager}},
                new EventAggregator(),
                contentManager);
        }
    }
}