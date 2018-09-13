using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Events;
using Microsoft.Xna.Framework.Input;

namespace Core.Components
{
    public class KeyboardInputHandler : ComponentDecorator, ISubscriber<KeyboardState>
    {
        public ICollection<KeyValuePair<Func<KeyboardState, bool>, Action<KeyboardState, IComponentDependencies> > >  InputHandlers;
        
        public KeyboardInputHandler(IComponent baseComponent, params Type[] requiredComponents) : this(baseComponent, requiredComponents.ToList()) {}
        
        public KeyboardInputHandler(IComponent baseComponent, IList<Type> requiredComponents, bool enabled = true) : base(baseComponent)
        {
            InputHandlers =
                new List<KeyValuePair<Func<KeyboardState, bool>, Action<KeyboardState, IComponentDependencies>>>();
            RequiredComponents = new ReadOnlyCollection<Type>(requiredComponents);
            Enabled = enabled;
        }

        public void AddEventListener(Func<KeyboardState, bool> conditionCheck, Action<KeyboardState, IComponentDependencies> actionToExecute)
        {
            InputHandlers.Add(new KeyValuePair<Func<KeyboardState, bool>, Action<KeyboardState, IComponentDependencies>>(conditionCheck, actionToExecute));
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