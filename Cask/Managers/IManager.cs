using Microsoft.Xna.Framework;

namespace Cask.Managers
{
    public interface IManager
    {
        bool Enabled { get; set; }
        void Update(GameTime gameTime);
    }
}