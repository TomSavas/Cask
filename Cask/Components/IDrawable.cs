using Microsoft.Xna.Framework;

namespace Cask.Components
{
    public interface IDrawable
    {
        uint Layer { get; set; }
        bool IsVisible { get; set; }

        void Draw(GameTime gameTime, Components.Camera camera);
    }
}