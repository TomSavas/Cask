using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Cask.Components;
using Microsoft.Xna.Framework.Content;

namespace Cask.GameObjects
{
    public class BasicGameObject : ComponentMap, IGameObject
    {
        public string Name { get; set; }
        public bool IsLoaded { get; private set; }
        
        public BasicGameObject(string name)
        {
            Name = name;
        }
        
        public override IComponentContainer AddComponent<T>(T component)
        {
            foreach (var dependencyType in component.RequiredComponents)
            {
                component.Dependencies.AddComponent(GetComponent(dependencyType));
            }

            // does it work this way?
            return base.AddComponent<T>(component);
        }

        public override IComponentContainer AddComponent(IComponent component)
        {
            foreach (var dependencyType in component.RequiredComponents)
            {
                component.Dependencies.AddComponent(GetComponent(dependencyType));
            }

            // does it work this way?
            return base.AddComponent(component);
        }

        public override IComponentContainer AddComponents(IEnumerable<IComponent> components)
        {
            foreach (var component in components)
            {
                AddComponent(component);
            }
            
            return this;
        }

        public override bool RemoveComponent<T>()
        {
            // If any dependency is broken, refuse to delete the component
            if(_components.Any(x => x.Value.RequiredComponents.Contains(typeof(T))))
                return false;

            return base.RemoveComponent<T>();
        }
        
        public virtual bool LoadContent(ContentManager contentManager)
        {
            IsLoaded = true;
            foreach (var component in GetComponents())
            {
                if(component is ILoadable loadable && !loadable.IsLoaded)
                    IsLoaded &= loadable.LoadContent(contentManager);
            }

            return IsLoaded;
        }
        
        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in GetComponents())
            {
                component.Update(gameTime);
            }
        }
    }
}