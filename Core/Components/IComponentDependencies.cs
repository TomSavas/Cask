using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Core.Components
{
    public interface IComponentDependencies
    {
        void Add<T>(T component) where T : class, IComponent;
        void Add(Type type, IComponent component);
        T Get<T>() where T : class, IComponent;
        bool Remove<T>() where T : class, IComponent;
        bool Contains<T>() where T : class, IComponent;
    }
}