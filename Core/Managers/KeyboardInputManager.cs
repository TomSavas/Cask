using System;
using Core.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Converters;

namespace Core.Managers
{
    public class KeyboardInputManager : IManager
    {
        private IEventAggregator _eventAggregator;

        public bool Enabled { get; set; }
        
        public KeyboardInputManager(IEventAggregator eventAggregator, bool enabled = true)
        {
            _eventAggregator = eventAggregator;
            Enabled = enabled;
        }
        
        public void Update(GameTime gameTime)
        {
            if (!Enabled) return;
            
            _eventAggregator.PublishEvent(Keyboard.GetState());
        }
    }
}
