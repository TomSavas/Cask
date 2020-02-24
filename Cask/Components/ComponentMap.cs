using System;
using System.Collections.Generic;

namespace Cask.Components
{
    public class ComponentMap : IComponentContainer
    {
        protected readonly Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

        public virtual IComponentContainer AddComponent<T>(T component) where T : class, IComponent
        {
            _components[typeof(T)] = component;
            
            return this;
        }

        public virtual IComponentContainer AddComponent(IComponent component)
        {
            _components[component.GetType()] = component;

            return this;
        }

        public virtual IComponentContainer AddComponents(IEnumerable<IComponent> components)
        {
            foreach (var component in components)
                _components.Add(component.GetType(), component);

            return this;
        }

        public virtual T GetComponent<T>() where T : class, IComponent => _components[typeof(T)] as T;
        
        public virtual IComponent GetComponent(Type componentType) => _components[componentType];

        public virtual IReadOnlyCollection<IComponent> GetComponents() => _components.Values;

        public virtual bool RemoveComponent<T>() where T : class, IComponent => _components.Remove(typeof(T));

        public virtual bool ContainsComponent<T>() where T : class, IComponent => _components.ContainsKey(typeof(T));
    }
}