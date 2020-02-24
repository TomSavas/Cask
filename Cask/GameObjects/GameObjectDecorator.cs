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
            Name = name;
        }

        public virtual T GetComponent<T>() where T : class, IComponent => 
            _underlyingGameObject.GetComponent<T>();
        
        public virtual IComponent GetComponent(Type componentType) => 
            _underlyingGameObject.GetComponent(componentType);

        public virtual IReadOnlyCollection<IComponent> GetComponents() =>
            _underlyingGameObject.GetComponents();

        public virtual IComponentContainer AddComponent<T>(T component) where T : class, IComponent =>
            _underlyingGameObject.AddComponent<T>(component);

        public virtual IComponentContainer AddComponent(IComponent component) =>
            _underlyingGameObject.AddComponent(component);
        
        public virtual IComponentContainer AddComponents(IEnumerable<IComponent> components) =>
            _underlyingGameObject.AddComponents(components);

        public virtual bool RemoveComponent<T>() where T : class, IComponent =>
            _underlyingGameObject.RemoveComponent<T>();

        public bool ContainsComponent<T>() where T : class, IComponent =>
            _underlyingGameObject.ContainsComponent<T>();

        public virtual bool LoadContent(ContentManager contentManager) => 
            _underlyingGameObject.LoadContent(contentManager);

        public virtual void Update(GameTime gameTime) => 
            _underlyingGameObject.Update(gameTime);
    }
}