using Cask.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cask.Managers
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
