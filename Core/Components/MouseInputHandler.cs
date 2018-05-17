using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Core.Events;
using Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Core.Components
{
    public class MouseInputHandler : ComponentDecorator, ISubscriber<MouseState>
    {
        public IDictionary<MouseState, ICollection<Action<IComponentDependencies, MouseState>>> InputHandlers = new Dictionary<MouseState, ICollection<Action<IComponentDependencies, MouseState>>>();
        
        public MouseInputHandler(IComponent baseComponent, params Type[] requiredComponents) : this(baseComponent, requiredComponents.ToList()) {}
        
        public MouseInputHandler(IComponent baseComponent, IList<Type> requiredComponents, bool enabled = true) : base(baseComponent)
        {
            InputHandlers = new Dictionary<MouseState, ICollection<Action<IComponentDependencies, MouseState>>>();
            RequiredComponents = new ReadOnlyCollection<Type>(requiredComponents);
            Enabled = enabled;
        }

        public void OnEventHandler(MouseState mouseState)
        {
            if (!Enabled) return;
            
            foreach (var state in InputHandlers.Keys)
                if(state.LeftButton == mouseState.LeftButton || state.RightButton == mouseState.RightButton || state.MiddleButton == mouseState.MiddleButton)
                    foreach (var inputHandler in InputHandlers[state])
                        inputHandler(Dependencies, mouseState); 
        }
    }
}