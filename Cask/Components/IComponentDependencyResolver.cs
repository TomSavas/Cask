using System;
using System.Collections;
using System.Collections.Generic;

namespace Cask.Components
{
    public interface IComponentDependencyResolver
    {
        void ResolveDependencies(IComponent component, IDictionary<Type, IComponent> existingComponents);
        void RemoveDependencies(IComponent component, IDictionary<Type, IComponent> existingComponents);
    }
}