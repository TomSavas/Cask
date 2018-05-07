using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OpenJam.Core.GameObjects;
using OpenJam.Core.Managers;

namespace OpenJam.Core
{
    public interface IScene
    {
        ICollection<IGameObject> GameObjects { get; }
        IDictionary<Type, IManager> GameManagers { get; }
        ICamera MainCamera { get; }

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}