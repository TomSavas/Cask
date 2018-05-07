using System;
using System.Collections.Generic;
using Core.Components;
using Microsoft.Xna.Framework;
using IDrawable = Core.Components.IDrawable;

namespace Core.GameObjects
{
    public interface IGameObject : ILoadable
    {
        string Name { get; }
        IReadOnlyCollection<Components.IDrawable> Drawables { get; }
        IReadOnlyDictionary<Type, IComponent> Components { get; }

        void AddComponent<T>(T component) where T : class, IComponent;
        bool RemoveComponent<T>(T component) where T : class, IComponent;
        
        void Update(GameTime gameTime);
    }
}