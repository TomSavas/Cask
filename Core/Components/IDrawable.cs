using Microsoft.Xna.Framework;

namespace Core.Components
{
    public interface IDrawable : IComponent
    {
        uint Layer { get; set; }
        bool IsVisible { get; set; }

        void Draw(GameTime gameTime, Components.Camera camera);
    }
}