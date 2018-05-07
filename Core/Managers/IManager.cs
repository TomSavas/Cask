using Microsoft.Xna.Framework;

namespace Core.Managers
{
    public interface IManager
    {
        bool Enabled { get; set; }
        void Update(GameTime gameTime);
    }
}