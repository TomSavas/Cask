using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Cask.Scenes
{
    public interface ISceneManager
    {
        IDictionary<string, IScene> Scenes { get; }
        IScene CurrentScene { get; }
        IScene DefaultLoadingScene { get; set; }
        bool IsLoading { get; }
        
        void LoadScene(string sceneName, bool useLoadingScene = true);
        void PreloadScene(string sceneName);

        bool LoadContent(ContentManager contentManager);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}