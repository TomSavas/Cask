using Core.Components;
using Core.GameObjects;
using Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Factories
{
    public interface IGameElementFactory
    {
        IComponent MakeComponent();
        IGameObject MakeGameObject(string name = "GameObject");
        IGameObject MakeCamera(GraphicsDeviceManager graphicsDeviceManager);
        IScene MakeScene(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager);
    }
}