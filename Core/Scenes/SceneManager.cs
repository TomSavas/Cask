using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Scenes
{
    public class SceneManager : ISceneManager
    {
        public ContentManager ContentManager { get; set; }
        
        private IScene _currentScene;
        private IDictionary<string, IScene> _scenes = new Dictionary<string, IScene>();
        private IScene _defaultLoadingScene;
        private IScene _sceneBeingLoaded;
        private bool _isLoading;
        private Thread _loadingThread;

        public SceneManager(IScene mainScene, ContentManager contentManager)
        {
            ContentManager = contentManager;
            
            AddScene("MainScene", mainScene);
        }
        
        public void LoadScene(string sceneName, bool useLoadingScene = false)
        {
            if (useLoadingScene)
            {
                LoadScene(sceneName, _defaultLoadingScene);
            } else
            {
                _currentScene = _scenes[sceneName];
                LoadContent(ContentManager);
            }
        }

        public void LoadScene(string sceneName, IScene loadingScene)
        {
            _isLoading = true;
            PreloadScene(sceneName);
            _currentScene = loadingScene;
        }

        public void PreloadScene(string sceneName)
        {
            _sceneBeingLoaded = _scenes[sceneName];
            _loadingThread = new Thread(() => _sceneBeingLoaded.LoadContent(ContentManager));
            _loadingThread.Start();
        }

        public void PreloadScene(string sceneName, IScene scene)
        {
            AddScene(sceneName, scene);
            PreloadScene(sceneName);
        }

        public void UnloadScene(string sceneName)
        {
            throw new NotImplementedException();
        }
        
        public void AddScene(string sceneName, IScene scene)
        {
            _scenes.Add(sceneName, scene);
        }

        public bool RemoveScene(string sceneName)
        {
            return _scenes.Remove(sceneName);
        }

        public void SetLoadingScene(IScene scene)
        {
            _defaultLoadingScene = scene;
        }

        public bool LoadContent(ContentManager contentManager)
        {
            return _currentScene.LoadContent(contentManager);
        }

        public void Update(GameTime gameTime)
        {
            if (_isLoading && _loadingThread != null && !_loadingThread.IsAlive)
            {
                //Handle possible transitions between scenes
                _currentScene = _sceneBeingLoaded;
                _isLoading = false;
            }
            
            _currentScene.Update(gameTime);
        }
        
        public void Draw(GameTime gameTime) => _currentScene.Draw(gameTime);
    }
}