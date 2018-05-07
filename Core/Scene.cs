using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using OpenJam.Core.Extensions;
using OpenJam.Core.GameObjects;
using OpenJam.Core.Managers;

namespace OpenJam.Core
{
    public class Scene : IScene
    {
        public ICollection<IGameObject> GameObjects { get; }
        public IDictionary<Type, IManager> GameManagers { get; }
        public ICamera MainCamera { get; set; }

        public Scene(ICamera mainCamera, IDrawingManager drawingManager)
        {
            GameObjects = new List<IGameObject>();
            GameManagers = new Dictionary<Type, IManager>();
            MainCamera = mainCamera;
            
            drawingManager.Cameras.Add(mainCamera.Components.Get<Components.Camera>());
            GameManagers.Add<IDrawingManager>(drawingManager);
        }
        
        public virtual void Update(GameTime gameTime) => 
            GameObjects
                .ToList()
                .ForEach(gameObject => gameObject.Update(gameTime));

        public virtual void Draw(GameTime gameTime) =>
            GameManagers.Get<IDrawingManager>().Draw(gameTime, GameObjects);

    }
}