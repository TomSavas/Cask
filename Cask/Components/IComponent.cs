using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Cask.Components
{
    public interface IComponent : ILoadable, IDrawable
    {
        IReadOnlyCollection<Type> RequiredComponents { get; }
        IComponentDependencies Dependencies { get; }
        bool Enabled { get; set; }

        void Update(GameTime gameTime);
    }
}