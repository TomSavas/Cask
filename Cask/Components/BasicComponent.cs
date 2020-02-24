using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Cask.Components
{
    public class BasicComponent : IComponent
    {
        public IReadOnlyCollection<Type> RequiredComponents { get; }
        public IComponentContainer Dependencies { get; }
        public bool Enabled { get; set; }
        public bool IsLoaded { get; }
        public bool IsVisible { get; set; }
        public uint Layer { get; set; }

        public BasicComponent()
        {
            RequiredComponents = new List<Type>();
            Dependencies = new ComponentMap();
            IsLoaded = true;
        }
        
        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in Dependencies.GetComponents())
            {
                component.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, Camera camera)
        {
            foreach (var component in Dependencies.GetComponents())
            {
                component.Draw(gameTime, camera);
            }           
        }
        
        public virtual bool LoadContent(ContentManager contentManager)
        {
            return IsLoaded;
        }
    }
}