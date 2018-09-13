using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

namespace Core.Components
{
    public class Map : ComponentDecorator
    {
        private readonly string _mapTitle;
        private TiledMap _map;
        private TiledMapRenderer _mapRenderer;
        
        public Map(IComponent baseComponent, string mapTitle) : base(baseComponent)
        {
            RequiredComponents = new ReadOnlyCollection<Type>(new List<Type>{typeof(Transform)});
            _mapTitle = mapTitle;
            Layer = 0;
        }

        public override bool LoadContent(ContentManager contentManager)
        {
            _map = contentManager.Load<TiledMap>(_mapTitle);
            IsLoaded = base.LoadContent(contentManager);
            
            return IsLoaded;
        }

        public override void Update(GameTime gameTime)
        {
            if(_mapRenderer != null)
                _mapRenderer.Update(_map, gameTime);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, Camera camera)
        {
            _mapRenderer = _mapRenderer ?? new TiledMapRenderer(camera.GraphicsDevice);
            _mapRenderer.Draw(_map, camera.GetViewMatrix());
            
            base.Draw(gameTime, camera);
        }
    }
}