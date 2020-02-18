using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Cask.Scenes
{
    public class SceneManager : ISceneManager
    {
        public IDictionary<string, IScene> Scenes { get; } = new Dictionary<string, IScene>();
        public IScene CurrentScene { get; private set; }
        public IScene DefaultLoadingScene { get; set; }
        public bool IsLoading { get; private set; }
        
        private ContentManager _contentManager { get; set; }
        private IScene _sceneBeingLoaded;
        private Thread _loadingThread;

        public SceneManager(IScene mainScene, ContentManager contentManager) : this(contentManager)
        {
            Scenes.Add("MainScene", mainScene);
        }

        public SceneManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }
        
        public void LoadScene(string sceneName, bool useDefaultLoadingScene = true)
        {
            if (useDefaultLoadingScene && DefaultLoadingScene != null)
            {
                IsLoading = true;
                PreloadScene(sceneName);
                CurrentScene = DefaultLoadingScene;                
            } else
            {
                CurrentScene = Scenes[sceneName];
            }

            LoadContent(_contentManager);
        }

        public void PreloadScene(string sceneName)
        {
            _sceneBeingLoaded = Scenes[sceneName];
            _loadingThread = new Thread(() => _sceneBeingLoaded.LoadContent(_contentManager));
            _loadingThread.Start();
        }

        public void UnloadScene(string sceneName)
        {
            throw new NotImplementedException();
        }
        
        public bool LoadContent(ContentManager contentManager)
        {
            return CurrentScene.LoadContent(contentManager);
        }

        public void Update(GameTime gameTime)
        {
            if (IsLoading && _loadingThread != null && !_loadingThread.IsAlive)
            {
                //Handle possible transitions between scenes
                CurrentScene = _sceneBeingLoaded;
                IsLoading = false;
            }

            CurrentScene.Update(gameTime);
        }
        
        public void Draw(GameTime gameTime) => CurrentScene.Draw(gameTime);
    }
}