using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Scenes
{
    public interface ISceneManager
    {
        void LoadScene(string sceneName, bool useLoadingScene = false);
        void LoadScene(string sceneName, IScene loadingScene);
        void PreloadScene(string sceneName);
        void PreloadScene(string sceneName, IScene scene);
        void AddScene(string sceneName, IScene scene);
        bool RemoveScene(string sceneName);
        void SetLoadingScene(IScene scene);

        bool LoadContent(ContentManager contentManager);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}