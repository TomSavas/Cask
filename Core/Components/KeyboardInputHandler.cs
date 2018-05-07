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
    public class KeyboardInputHandler : IComponent, ISubscriber<KeyboardState>
    {
        public ICollection<Type> RequiredComponents { get; }
        public IComponentDependencies Dependencies { get; } = new ComponentDependencies();
        public IDictionary<KeyboardState, ICollection<Action<IComponentDependencies>>> InputHandlers = new Dictionary<KeyboardState, ICollection<Action<IComponentDependencies>>>();
        
        public bool Enabled { get; set; }
        public bool IsLoaded => true;

        public KeyboardInputHandler(ICollection<Type> requiredComponents, bool enabled = true)
        {
            InputHandlers = new Dictionary<KeyboardState, ICollection<Action<IComponentDependencies>>>();
            RequiredComponents = requiredComponents;
            Enabled = enabled;
        }

        public KeyboardInputHandler(params Type[] requiredComponents) : this(requiredComponents.ToList()) {}

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

        public bool LoadContent(ContentManager contentManager) => true;
        public void Update(GameTime gameTime) {}
    }
}