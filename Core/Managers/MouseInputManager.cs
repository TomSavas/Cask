using System;
using Core.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Core.Managers
{
    public class MouseInputManager : IManager
    {
        private IEventAggregator _eventAggregator;

        public bool Enabled { get; set; }
        
        public MouseInputManager(IEventAggregator eventAggregator, bool enabled = true)
        {
            _eventAggregator = eventAggregator;
            Enabled = enabled;
        }
        
        public void Update(GameTime gameTime)
        {
            if (!Enabled) return;
            
            _eventAggregator.PublishEvent(Mouse.GetState());
        }
    }
}