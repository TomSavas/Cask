using System;
using System.Collections.Generic;
using Core.Events;
using Microsoft.Xna.Framework.Input;

namespace Cask.Components
{
    public class KeyboardInputHandler : ComponentDecorator, ISubscriber<KeyboardState>
    {
        public ICollection<KeyValuePair<Func<KeyboardState, bool>, Action<KeyboardState, IComponentContainer> > >  InputHandlers;
        
        public KeyboardInputHandler(IComponent baseComponent, bool enabled = true) : base(baseComponent, typeof(Transform))
        {
            InputHandlers = new List<KeyValuePair<Func<KeyboardState, bool>, Action<KeyboardState, IComponentContainer>>>();
            Enabled = enabled;
        }

        public void AddEventListener(Func<KeyboardState, bool> conditionCheck, Action<KeyboardState, IComponentContainer> actionToExecute)
        {
            InputHandlers.Add(new KeyValuePair<Func<KeyboardState, bool>, Action<KeyboardState, IComponentContainer>>(conditionCheck, actionToExecute));
        }

        public void OnEventHandler(KeyboardState keyboardState)
        {
            if (!Enabled) return;
            
            foreach (var inputHandler in InputHandlers)
                if (inputHandler.Key(keyboardState))
                        inputHandler.Value(keyboardState, Dependencies); 
        }
    }
}