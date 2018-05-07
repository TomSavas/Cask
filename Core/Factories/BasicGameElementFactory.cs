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

namespace Core.Factories
{
    public class BasicGameElementFactory
    {
        public IGameObject MakeCamera(GraphicsDeviceManager graphicsDeviceManager)
        {
            return new GameObject("Camera",
                new List<IComponent> {new Transform(), new Camera(graphicsDeviceManager)});
        }
        
        public IScene MakeScene(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager)
        {
            var camera = MakeCamera(graphicsDeviceManager);
            var drawingManager = new DrawingManager(camera.Components.Get<Camera>());
            
            return new Scene(new List<IGameObject> {camera},
                new Dictionary<Type, IManager> {{typeof(IDrawingManager), drawingManager}},
                new EventAggregator(),
                contentManager);
        }
    }
}