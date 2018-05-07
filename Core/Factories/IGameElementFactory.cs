using Core.GameObjects;
using Core.Scenes;
using Microsoft.Xna.Framework.Content;

namespace Core.Factories
{
    public interface IGameElementFactory
    {
        IGameObject MakeCameraObject();
        IScene MakeScene(ContentManager contentManager);
    }
}