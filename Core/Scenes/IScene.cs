using System;
using System.Collections.Generic;
using Core.Components;
using Core.Events;
using Core.GameObjects;
using Core.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Scenes
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