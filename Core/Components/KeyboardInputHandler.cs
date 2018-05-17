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
    public class KeyboardInputHandler : ComponentDecorator, ISubscriber<KeyboardState>
    {
        public IDictionary<KeyboardState, ICollection<Action<IComponentDependencies>>> InputHandlers = new Dictionary<KeyboardState, ICollection<Action<IComponentDependencies>>>();
        
        public KeyboardInputHandler(IComponent baseComponent, params Type[] requiredComponents) : this(baseComponent, requiredComponents.ToList()) {}
        
        public KeyboardInputHandler(IComponent baseComponent, IList<Type> requiredComponents, bool enabled = true) : base(baseComponent)
        {
            InputHandlers = new Dictionary<KeyboardState, ICollection<Action<IComponentDependencies>>>();
            RequiredComponents = new ReadOnlyCollection<Type>(requiredComponents.ToList());
            Enabled = enabled;
        }

        public void OnButtonDown(Keys key, Action<IComponentDependencies> onButtonDownAction)
        {
            OnButtonsDown(new List<Keys> {key}, onButtonDownAction);
        }
        
        public void OnButtonsDown(ICollection<Keys> keys, Action<IComponentDependencies> onButtonDownAction)
        {
            if (!Enabled) return;
            
            InputHandlers.Add(new KeyboardState(keys.ToArray()), new List<Action<IComponentDependencies>>
            {
                onButtonDownAction
            });
        }

        public void OnEventHandler(KeyboardState keyboardState)
        {
            if (!Enabled) return;
            
            foreach (var state in InputHandlers.Keys)
                if (keyboardState.GetPressedKeys().Any(key => state.GetPressedKeys().Contains(key)))
                    foreach (var inputHandler in InputHandlers[state])
                        inputHandler(Dependencies); 
        }
    }
}