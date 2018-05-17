using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Core.Components;
using Microsoft.Xna.Framework.Content;

namespace Core.GameObjects
{
    public class BasicGameObject : IGameObject
    {
        public string Name { get; set; }
        public bool IsLoaded { get; private set; }
        
        private Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();
        private IComponentDependencyResolver _componentDependencyResolver;

        public BasicGameObject(string name, ICollection<IComponent> components, IComponentDependencyResolver componentDependencyResolver)
        {
            Name = name;
            _componentDependencyResolver = componentDependencyResolver;
            AddComponents(components);
        }
        
        public T GetComponent<T>() where T : class, IComponent => _components[typeof(T)] as T;

        IReadOnlyDictionary<Type, IComponent> IGameObject.GetComponents()
        {
            return new ReadOnlyDictionary<Type, IComponent>(_components);
        }

        public void AddComponent<T>(T component) where T : class, IComponent
        {
            _componentDependencyResolver.ResolveDependencies(component, _components);
            _components.Add(component.GetType(), component);
        }

        public void AddComponents(ICollection<IComponent> components)
        {
            _components = components.Aggregate(_components, (aggregate, element) =>
            {
                aggregate.Add(element.GetType(), element);
                return aggregate;
            });

            foreach (var component in _components.Values)
            {
                _componentDependencyResolver.ResolveDependencies(component, _components);
            }
        }

        public bool RemoveComponent<T>(T component) where T : class, IComponent
        {
            _componentDependencyResolver.RemoveDependencies(component, _components);
            return _components.Remove(component.GetType());
        }

        
        public virtual bool LoadContent(ContentManager contentManager)
        {
            IsLoaded = !_components.Values
                .OfType<ILoadable>()
                .Where(loadable => !loadable.IsLoaded)
                .AsParallel()
                .Select(loadable => loadable.LoadContent(contentManager))
                .Any(response => response == false);

            return IsLoaded;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in _components.Values)
            {
                component.Update(gameTime);
            }
        }
    }
}