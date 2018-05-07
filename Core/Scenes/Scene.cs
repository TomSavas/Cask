using System;
using System.Collections;
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
    public class Scene : IScene
    {
        public ICollection<IGameObject> GameObjects { get; set; }
        public IDictionary<Type, IManager> GameManagers { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public ContentManager ContentManager { get; set; }
        public bool IsLoaded { get; private set; }

        public Scene(ICollection<IGameObject> gameObjects, IDictionary<Type, IManager> gameManagers, IEventAggregator eventAggregator, ContentManager contentManager)
        {
            GameObjects = gameObjects;
            GameManagers = gameManagers;
            EventAggregator = eventAggregator;
            ContentManager = contentManager;
        }
        
        //public Scene(IDrawingManager drawingManager,
        //    IEventAggregator eventAggregator, params IGameObject[] gameObjects) : this(new List<IGameObject>(gameObjects), new Dictionary<Type, IManager>(), drawingManager,
        //    eventAggregator) {}
 
        //public Scene(IDrawingManager drawingManager, IEventAggregator eventAggregator, params IManager[] gameManagers)
        //        : this(new List<IGameObject>(), 
        //            gameManagers.Aggregate(new Dictionary<Type, IManager>(),
        //                (accumulator, manager) =>
        //                {
        //                    accumulator.Add(manager.GetType(), manager);
        //                    return accumulator;
        //                }),
        //            drawingManager,
        //            eventAggregator) {}

        //public Scene(IDrawingManager drawingManager, IEventAggregator eventAggregator) : this(new List<IGameObject>(), new Dictionary<Type, IManager>(), drawingManager, eventAggregator) {}

        //public Scene(IDrawingManager drawingManager) : this(drawingManager, new EventAggregator()) {}

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
            foreach (var updatables in GameObjects.Zip(GameManagers.Values, (gameObject, manager) => new KeyValuePair<IGameObject, IManager>(gameObject, manager)))
            {
                updatables.Key.Update(gameTime);
                updatables.Value.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (!GameManagers.ContainsKey(typeof(IDrawingManager))) return;
            
            GameManagers.Get<IDrawingManager>().Draw(gameTime, GameObjects);
        }
    }
}