using System;
using System.Collections.Generic;
using Core.Events;
using Microsoft.Xna.Framework.Input;

namespace Cask.Components
{
    public class MouseInputHandler : ComponentDecorator, ISubscriber<MouseState>
    {
        public ICollection<KeyValuePair<Func<MouseState, bool>, Action<MouseState, IComponentContainer> > >  InputHandlers;
        
        public MouseInputHandler(IComponent baseComponent, bool enabled = true) : base(baseComponent, typeof(Transform), typeof(Text))
        {
            InputHandlers =
                new List<KeyValuePair<Func<MouseState, bool>, Action<MouseState, IComponentContainer>>>();
            Enabled = enabled;
        }

        public void AddEventListener(Func<MouseState, bool> conditionCheck, Action<MouseState, IComponentContainer> actionToExecute)
        {
            InputHandlers.Add(new KeyValuePair<Func<MouseState, bool>, Action<MouseState, IComponentContainer>>(conditionCheck, actionToExecute));
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