using System;
using System.Collections.Generic;
using Cask.Components;
using Microsoft.Xna.Framework;
using IDrawable = Cask.Components.IDrawable;

namespace Cask.GameObjects
{
    public interface IGameObject : ILoadable, IComponentContainer
    {
        string Name { get; set; }

        void Update(GameTime gameTime);
    }
}