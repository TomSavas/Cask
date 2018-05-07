using System.Collections.Generic;
using Core.GameObjects;
using Microsoft.Xna.Framework;

namespace Core.Managers
{
    public interface IDrawingManager : IManager
    {
        ICollection<Components.Camera> Cameras { get; }
        
        void Draw(GameTime gameTime, ICollection<IGameObject> gameObjects);
    }
}