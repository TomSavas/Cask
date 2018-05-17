using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Components
{
    public class BasicComponent : IComponent
    {
        public IReadOnlyCollection<Type> RequiredComponents { get; }
        public IComponentDependencies Dependencies { get; }
        public bool Enabled { get; set; }
        public bool IsLoaded { get; protected set; }
        public bool IsVisible { get; set; }
        public uint Layer { get; set; }

        public BasicComponent()
        {
            Dependencies = new ComponentDependencies();    
        }
        
        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in Dependencies.GetAll())
            {
                component.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, Camera camera)
        {
            foreach (var component in Dependencies.GetAll())
            {
                component.Draw(gameTime, camera);
            }           
        }
        
        public virtual bool LoadContent(ContentManager contentManager)
        {
            IsLoaded = Dependencies.GetAll().All(component => component.LoadContent(contentManager));
            return IsLoaded;
        }
    }
}