using System;
using System.Collections.Generic;
using Cask.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Cask.GameObjects
{
    public class GameObjectDecorator : IGameObject
    {
        public string Name
        {
            get => _underlyingGameObject.Name;
            set => _underlyingGameObject.Name = value;
        }
        
        public bool IsLoaded => _underlyingGameObject.IsLoaded;

        private IGameObject _underlyingGameObject;

        public GameObjectDecorator(IGameObject baseGameObject, string name = "GameObject") : this(baseGameObject, new List<IComponent>(), name) {}

        public GameObjectDecorator(IGameObject baseGameObject, ICollection<IComponent> components, string name = "GameObject")
        {
            _underlyingGameObject = baseGameObject;
            AddComponents(components);
            Name = Name;
        }

        public virtual T GetComponent<T>() where T : class, IComponent => 
            _underlyingGameObject.GetComponent<T>();

        public virtual IReadOnlyDictionary<Type, IComponent> GetComponents() =>
            _underlyingGameObject.GetComponents();

        public virtual void AddComponent<T>(T component) where T : class, IComponent =>
            _underlyingGameObject.AddComponent<T>(component);
        
        public virtual void AddComponents(ICollection<IComponent> components) =>
            _underlyingGameObject.AddComponents(components);

        public virtual bool RemoveComponent<T>(T component) where T : class, IComponent =>
            _underlyingGameObject.RemoveComponent<T>(component);
        
        public virtual bool LoadContent(ContentManager contentManager) => 
            _underlyingGameObject.LoadContent(contentManager);

        public virtual void Update(GameTime gameTime) => 
            _underlyingGameObject.Update(gameTime);
    }
}