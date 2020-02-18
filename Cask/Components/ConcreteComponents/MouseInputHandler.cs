using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cask.Events;
using Core.Events;
using Microsoft.Xna.Framework.Input;

namespace Cask.Components
{
    public class MouseInputHandler : ComponentDecorator, ISubscriber<MouseState>
    {
        public ICollection<KeyValuePair<Func<MouseState, bool>, Action<MouseState, IComponentDependencies> > >  InputHandlers;
        
        public MouseInputHandler(IComponent baseComponent, params Type[] requiredComponents) : this(baseComponent, requiredComponents.ToList()) {}
        
        public MouseInputHandler(IComponent baseComponent, IList<Type> requiredComponents, bool enabled = true) : base(baseComponent)
        {
            InputHandlers =
                new List<KeyValuePair<Func<MouseState, bool>, Action<MouseState, IComponentDependencies>>>();
            RequiredComponents = new ReadOnlyCollection<Type>(requiredComponents);
            Enabled = enabled;
        }

        public void AddEventListener(Func<MouseState, bool> conditionCheck, Action<MouseState, IComponentDependencies> actionToExecute)
        {
            InputHandlers.Add(new KeyValuePair<Func<MouseState, bool>, Action<MouseState, IComponentDependencies>>(conditionCheck, actionToExecute));
        }
        
        public void OnEventHandler(MouseState mouseState)
        {
            if (!Enabled) return;

            foreach (var inputHandler in InputHandlers)
            {
                if (inputHandler.Key(mouseState))
                    inputHandler.Value(mouseState, Dependencies);
            }
        }
    }
}