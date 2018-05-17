using System;
using System.Collections.Generic;
using Core.Components;
using Microsoft.Xna.Framework;
using IDrawable = Core.Components.IDrawable;

namespace Core.GameObjects
{
    public interface IGameObject : ILoadable
    {
        string Name { get; set; }

        T GetComponent<T>() where T : class, IComponent;
        IReadOnlyDictionary<Type, IComponent> GetComponents();
        void AddComponent<T>(T component) where T : class, IComponent;
        void AddComponents(ICollection<IComponent> components);
        bool RemoveComponent<T>(T component) where T : class, IComponent;
        
        void Update(GameTime gameTime);
    }
}