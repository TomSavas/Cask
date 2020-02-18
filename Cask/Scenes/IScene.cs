using System;
using System.Collections.Generic;
using Cask.Components;
using Cask.Events;
using Cask.GameObjects;
using Cask.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Cask.Scenes
{
    public interface IScene : ILoadable
    {
        ICollection<IGameObject> GameObjects { get; set; }
        IDictionary<Type, IManager> GameManagers { get; set; }
        IEventAggregator EventAggregator { get; set; }
        ContentManager ContentManager { get; set; }

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}