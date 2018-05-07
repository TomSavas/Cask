using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Core.Components
{
    public interface IComponent : ILoadable
    {
        ICollection<Type> RequiredComponents { get; }
        IComponentDependencies Dependencies { get; }
        bool Enabled { get; set; }

        void Update(GameTime gameTime);
    }
}