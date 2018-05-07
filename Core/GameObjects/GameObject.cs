using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Core.Components;
using Microsoft.Xna.Framework.Content;
using IDrawable = Core.Components.IDrawable;

namespace Core.GameObjects
{
    public class GameObject : IGameObject
    {
        public string Name { get; }
        public bool IsLoaded { get; private set; }
        public IReadOnlyCollection<IDrawable> Drawables => new ReadOnlyCollection<IDrawable>(_drawables);
        public IReadOnlyDictionary<Type, IComponent> Components => new ReadOnlyDictionary<Type, IComponent>(_components);
        
        private Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();
        private List<IDrawable> _drawables = new List<IDrawable>();
        private IComponentDependencyResolver _componentDependencyResolver;

        public GameObject() : this("GameObject", new List<IComponent>()) {}
        public GameObject(ICollection<IComponent> components) : this("GameObject", components) {}
        public GameObject(string name, ICollection<IComponent> components) : this(name, components, new ComponentDependencyResolver()) {}

        public GameObject(string name, ICollection<IComponent> components, IComponentDependencyResolver componentDependencyResolver)
        {
            Name = name;
            _componentDependencyResolver = componentDependencyResolver;

            _components = components.Aggregate(new Dictionary<Type, IComponent>(), (aggregate, element) =>
            {
                aggregate.Add(element.GetType(), element);
                return aggregate;
            });

            foreach (var component in _components.Values)
            {
                _componentDependencyResolver.ResolveDependencies(component, _components);
                if (typeof(IDrawable).IsAssignableFrom(component.GetType()))
                {
                    _drawables.Add((IDrawable)component);
                }
            }
        }
        
        public void AddComponent<T>(T component) where T : class, IComponent
        {
            _componentDependencyResolver.ResolveDependencies(component, _components);
            _components.Add(component.GetType(), component);

            if (typeof(IDrawable).IsAssignableFrom(component.GetType()))
            {
                _drawables.Add((IDrawable)component);
            }
        }
        
        public bool RemoveComponent<T>(T component) where T : class, IComponent
        {
            _componentDependencyResolver.RemoveDependencies(component, _components);
            bool removeStatus = _components.Remove(component.GetType());
            if (removeStatus && typeof(IDrawable).IsAssignableFrom(component.GetType()))
                _drawables.Remove((IDrawable) component);
            return removeStatus;
        }

        public T GetComponent<T>() where T : class, IComponent => _components[typeof(T)] as T;
        
        public virtual bool LoadContent(ContentManager contentManager)
        {
            IsLoaded = !_components.Values
                .Where(type => typeof(ILoadable).IsAssignableFrom(type.GetType()))
                .Cast<ILoadable>()
                .Where(loadable => !loadable.IsLoaded)
                .Select(loadable => loadable.LoadContent(contentManager))
                .AsParallel()
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