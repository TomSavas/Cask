using System;
using System.Collections.Generic;
using Core.Components;
using Core.Events;
using Core.Extensions;
using Core.GameObjects;
using Core.Managers;
using Core.Scenes;
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
            return new BasicGameObject(name, new List<IComponent>(), new ComponentDependencyResolver());
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