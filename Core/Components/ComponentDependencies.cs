using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Components
{
    public class ComponentDependencies : IComponentDependencies
    {
        private IDictionary<Type, WeakReference> _dependencies = new Dictionary<Type, WeakReference>();
        
        public void Add<T>(T component) where T : class, IComponent
        {
            Add(typeof(T), component);
        }

        public void Add(Type type, IComponent component)
        {
            _dependencies.Add(type, new WeakReference(component));
        }

        public T Get<T>() where T : class, IComponent
        {
            var dependencyRef = _dependencies[typeof(T)] as WeakReference;
            if (dependencyRef.IsAlive)
                return dependencyRef.Target as T;
            else
            //FIX: eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeech
                throw new Exception("Dependency broken");
        }

        public ICollection<IComponent> GetAll()
        {
            return _dependencies.Values.Select(componentRef => componentRef.Target as IComponent).ToList();
        }

        public bool Remove<T>() where T : class, IComponent
        {
            return _dependencies.Remove(typeof(T));
        }

        public bool Contains<T>() where T : class, IComponent
        {
            return _dependencies.ContainsKey(typeof(T));
        }
    }
}