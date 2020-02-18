using System;
using System.Collections.Generic;
using Cask.Events;
using Cask.GameObjects;
using Cask.Managers;
using Cask.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Scenes
{
    public class SceneDecorator : IScene
    {
        public ICollection<IGameObject> GameObjects
        {
            get => _underlyingScene.GameObjects;
            set => _underlyingScene.GameObjects = value;
        }

        public IDictionary<Type, IManager> GameManagers
        {
            get => _underlyingScene.GameManagers;
            set => _underlyingScene.GameManagers = value;
        }

        public IEventAggregator EventAggregator
        {
            get => _underlyingScene.EventAggregator;
            set => _underlyingScene.EventAggregator = value;
        }
        
        public ContentManager ContentManager
        {
            get => _underlyingScene.ContentManager;
            set => _underlyingScene.ContentManager = value;
        }

        public bool IsLoaded => _underlyingScene.IsLoaded;
        
        private IScene _underlyingScene;
        
        public SceneDecorator(IScene scene)
        {
            _underlyingScene = scene;
        }

        public virtual bool LoadContent(ContentManager contentManager) => _underlyingScene.LoadContent(contentManager);

        public virtual void Update(GameTime gameTime) => _underlyingScene.Update(gameTime);

        public virtual void Draw(GameTime gameTime) => _underlyingScene.Draw(gameTime);
    }
}