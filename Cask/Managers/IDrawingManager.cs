using System.Collections.Generic;
using Cask.GameObjects;
using Microsoft.Xna.Framework;

namespace Cask.Managers
{
    public interface IDrawingManager : IManager
    {
        ICollection<Components.Camera> Cameras { get; }
        
        void Draw(GameTime gameTime, ICollection<IGameObject> gameObjects);
    }
}