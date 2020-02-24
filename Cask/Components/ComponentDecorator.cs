using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Cask.Components
{
    public abstract class ComponentDecorator : IComponent
    {
        public IReadOnlyCollection<Type> RequiredComponents { get; }
        
        public IComponentContainer Dependencies => _underlyingComponent.Dependencies;
        public bool IsLoaded { get; protected set; }
        public bool Enabled { get => _underlyingComponent.Enabled; set => _underlyingComponent.Enabled = value; }
        public bool IsVisible { get => _underlyingComponent.IsVisible; set => _underlyingComponent.IsVisible = value; }
        public uint Layer { get => _underlyingComponent.Layer; set => _underlyingComponent.Layer = value; }

        protected IComponent _underlyingComponent;

        
        protected ComponentDecorator(IComponent baseComponent, params Type[] requiredComponents)
        {
            _underlyingComponent = baseComponent;
            
            RequiredComponents = new List<Type>(_underlyingComponent.RequiredComponents).Union(requiredComponents).ToList();
        }

        public virtual void Update(GameTime gameTime) => _underlyingComponent.Update(gameTime);

        public virtual void Draw(GameTime gameTime, Camera camera) => _underlyingComponent.Draw(gameTime, camera);

        public virtual bool LoadContent(ContentManager contentManager) => _underlyingComponent.LoadContent(contentManager);
    }
}