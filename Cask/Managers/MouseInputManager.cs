using System;
using Cask.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cask.Managers
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