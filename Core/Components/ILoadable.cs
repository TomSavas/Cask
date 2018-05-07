using Microsoft.Xna.Framework.Content;

namespace Core.Components
{
    public interface ILoadable
    {
        bool IsLoaded { get; }
        bool LoadContent(ContentManager contentManager);
    }
}