using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Components
{
    public class ComponentDecorator : IComponent
    {
        public ICollection<Type> RequiredComponents => _underlyingComponent.RequiredComponents;
        public IComponentDependencies Dependencies => _underlyingComponent.Dependencies;
        public bool Enabled
        {
            get => _underlyingComponent.Enabled;
            set => _underlyingComponent.Enabled = value;
        }
        public bool IsLoaded => _underlyingComponent.IsLoaded;
        
        private IComponent _underlyingComponent;

        public ComponentDecorator(IComponent component, ICollection<Type> requiredComponents, params IComponent[] dependencies)
        {
            _underlyingComponent = component;
            requiredComponents.AsParallel().ForAll(comp => RequiredComponents.Add(comp));
            dependencies.AsParallel().ForAll(dep => Dependencies.Add(dep));
        }

        public bool LoadContent(ContentManager contentManager) => _underlyingComponent.LoadContent(contentManager);

        public void Update(GameTime gameTime) => _underlyingComponent.Update(gameTime);
    }
}