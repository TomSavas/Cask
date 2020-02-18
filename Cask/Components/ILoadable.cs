using Microsoft.Xna.Framework.Content;

namespace Cask.Components
{
    public interface ILoadable
    {
        bool IsLoaded { get; }
        bool LoadContent(ContentManager contentManager);
    }
}