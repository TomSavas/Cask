using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core.Events;
using Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Core.Components
{
    public class MouseInputHandler : IComponent, ISubscriber<MouseState>
    {
        public ICollection<Type> RequiredComponents { get; } = new List<Type>();
        public IComponentDependencies Dependencies { get; } = new ComponentDependencies();
        public IDictionary<MouseState, ICollection<Action<IComponentDependencies, MouseState>>> InputHandlers = new Dictionary<MouseState, ICollection<Action<IComponentDependencies, MouseState>>>();
        
        public bool Enabled { get; set; }
        public bool IsLoaded => true;

        public MouseInputHandler(ICollection<Type> requiredComponents, bool enabled = true)
        {
            InputHandlers = new Dictionary<MouseState, ICollection<Action<IComponentDependencies, MouseState>>>();
            RequiredComponents = requiredComponents;
            Enabled = enabled;
        }
        
        public MouseInputHandler(params Type[] requiredComponents) : this(requiredComponents.ToList()) {}

        public void OnEventHandler(MouseState mouseState)
        {
            if (!Enabled) return;
            
            foreach (var state in InputHandlers.Keys)
                if(state.LeftButton == mouseState.LeftButton || state.RightButton == mouseState.RightButton || state.MiddleButton == mouseState.MiddleButton)
                    foreach (var inputHandler in InputHandlers[state])
                        inputHandler(Dependencies, mouseState); 

        }

        public bool LoadContent(ContentManager contentManager) => true;
        public void Update(GameTime gameTime) {}
    }
}