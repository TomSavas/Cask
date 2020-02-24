using System;
using System.Collections.Generic;

namespace Cask.Components
{
    public interface IComponentContainer
    {
        IComponentContainer AddComponent<T>(T component) where T : class, IComponent;
        IComponentContainer AddComponent(IComponent component);
        IComponentContainer AddComponents(IEnumerable<IComponent> components);
        T GetComponent<T>() where T : class, IComponent;
        IComponent GetComponent(Type componentType);
        IReadOnlyCollection<IComponent> GetComponents();
        bool RemoveComponent<T>() where T : class, IComponent;
        bool ContainsComponent<T>() where T : class, IComponent;
    }
}