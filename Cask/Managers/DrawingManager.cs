using System.Collections.Generic;
using Cask.Components;
using Cask.GameObjects;
using Microsoft.Xna.Framework;

namespace Cask.Managers
{
    public class DrawingManager : IDrawingManager
    {
        public ICollection<Camera> Cameras { get; }
        public bool Enabled { get; set; }

        public DrawingManager(Camera camera, bool enabled = true)
        {
            Enabled = enabled;
            Cameras = new List<Camera> {camera};
        }
        
        public void Update(GameTime gameTime) {}

        public void Draw(GameTime gameTime, ICollection<IGameObject> gameObjects)
        {
            if(!Enabled) return;
            
            Clear();
            
            var layeredDrawables = GetLayeredDrawables(gameObjects);
            for (int i = 0; i < layeredDrawables.Count; i++)
            {
                layeredDrawables[i]
                    .ForEach(drawable => Draw(gameTime, drawable));
            }
        }

        private void Clear()
        {
            foreach (var camera in Cameras)
            {
                camera.GraphicsDevice.Clear(Color.Black);
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