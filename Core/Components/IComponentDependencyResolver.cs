using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.Components
{
    public interface IComponentDependencyResolver
    {
        void ResolveDependencies(IComponent component, IDictionary<Type, IComponent> existingComponents);
        void RemoveDependencies(IComponent component, IDictionary<Type, IComponent> existingComponents);
    }
}