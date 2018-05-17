using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Components
{
    public class ComponentDecorator : IComponent
    {
        public IReadOnlyCollection<Type> RequiredComponents { get; protected set; }
        public IComponentDependencies Dependencies => _underlyingComponent.Dependencies;
        public bool IsLoaded { get; protected set; }
        public bool Enabled { get => _underlyingComponent.Enabled; set => _underlyingComponent.Enabled = value; }
        public bool IsVisible { get => _underlyingComponent.IsVisible; set => _underlyingComponent.IsVisible = value; }
        public uint Layer { get => _underlyingComponent.Layer; set => _underlyingComponent.Layer = value; }

        protected IComponent _underlyingComponent;

        public ComponentDecorator(IComponent baseComponent, params IComponent[] dependencies) : this(baseComponent, new List<Type>(), dependencies) {}
        
        public ComponentDecorator(IComponent baseComponent, IList<Type> requiredComponentsFromParent, params IComponent[] dependencies)
        {
            _underlyingComponent = baseComponent;
            RequiredComponents = new ReadOnlyCollection<Type>(requiredComponentsFromParent);
            dependencies.AsParallel().ForAll(dep => Dependencies.Add(dep.GetType(), dep));
        }

        public virtual void Update(GameTime gameTime) => _underlyingComponent.Update(gameTime);

        public virtual void Draw(GameTime gameTime, Camera camera) => _underlyingComponent.Draw(gameTime, camera);

        public virtual bool LoadContent(ContentManager contentManager) => _underlyingComponent.LoadContent(contentManager);
    }
}