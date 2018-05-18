using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.GameObjects;
using Microsoft.Xna.Framework;

namespace Core.Managers
{
    public class DrawingManager : IDrawingManager
    {
        public ICollection<Camera> Cameras { get; }
        public bool Enabled { get; set; }

        public DrawingManager(Camera camera, bool enabled = true)
        {
            Enabled = enabled;
            Cameras = new List<Camera>();
            Cameras.Add(camera);
        }
        
        public void Update(GameTime gameTime) {}

        public void Draw(GameTime gameTime, ICollection<IGameObject> gameObjects)
        {
            if(!Enabled) return;
            
            Clear();
            
            var layeredDrawables = GetLayeredDrawables(gameObjects);
            for (int i = layeredDrawables.Count; i > 0; i--)
            {
                layeredDrawables[i-1]
                    .ForEach(drawable => Draw(gameTime, drawable));
            }
        }

        private void Clear()
        {
            foreach (var camera in Cameras)
            {
                camera.GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
            }
        }

        private List<List<Components.IDrawable>> GetLayeredDrawables(ICollection<IGameObject> gameObjects)
        {
            var layeredDrawables = new List<List<Components.IDrawable>>();
            
            foreach (var gameObject in gameObjects)
            {
                foreach (var drawable in gameObject.GetComponents().Values)
                {
                    if (drawable.Layer + 1 > layeredDrawables.Count)
                    {
                        var layerDiff = drawable.Layer - layeredDrawables.Count + 1;
                        while (layerDiff-- != 0)
                            layeredDrawables.Add(new List<Components.IDrawable>());
                    }
                    
                    layeredDrawables[(int)drawable.Layer].Add(drawable);
                }
            }

            return layeredDrawables;
        }
        
        private void Draw(GameTime gameTime, Components.IDrawable drawable)
        {
            foreach (var camera in Cameras)
            {
                drawable.Draw(gameTime, camera);
            }            
        }
    }
}