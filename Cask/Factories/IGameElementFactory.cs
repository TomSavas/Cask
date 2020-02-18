using Cask.Components;
using Cask.GameObjects;
using Cask.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Cask.Factories
{
    public interface IGameElementFactory
    {
        IComponent MakeComponent();
        IGameObject MakeGameObject(string name = "GameObject");
        IGameObject MakeCamera(GraphicsDeviceManager graphicsDeviceManager);
        IScene MakeScene(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager);
    }
}