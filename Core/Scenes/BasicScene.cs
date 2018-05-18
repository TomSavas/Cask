using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Events;
using Core.Extensions;
using Core.GameObjects;
using Core.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Scenes
{
    public class BasicScene : IScene
    {
        public ICollection<IGameObject> GameObjects { get; set; }
        public IDictionary<Type, IManager> GameManagers { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public ContentManager ContentManager { get; set; }
        public bool IsLoaded { get; private set; }

        public BasicScene(ICollection<IGameObject> gameObjects, IDictionary<Type, IManager> gameManagers, IEventAggregator eventAggregator, ContentManager contentManager)
        {
            GameObjects = gameObjects;
            GameManagers = gameManagers;
            EventAggregator = eventAggregator;
            ContentManager = contentManager;
        }
        
        public virtual bool LoadContent(ContentManager contentManager)
        {
            IsLoaded = GameObjects
                .Select(gameObject => gameObject.LoadContent(contentManager))
                .All(response => response != false);

            IsLoaded = IsLoaded &&
                       !GameManagers.Values
                           .Where(type => typeof(ILoadable).IsAssignableFrom(type.GetType()))
                           .Cast<ILoadable>()
                           .Where(loadable => loadable.IsLoaded)
                           .AsParallel()
                           .Select(loadable => loadable.LoadContent(contentManager))
                           .Any(response => response == false);

            return IsLoaded;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach(var gameManager in GameManagers.Values) 
                gameManager.Update(gameTime);

            foreach (var gameObject in GameObjects)
                gameObject.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (!GameManagers.ContainsKey(typeof(IDrawingManager))) return;
            
            GameManagers.Get<IDrawingManager>().Draw(gameTime, GameObjects);
        }
    }
}