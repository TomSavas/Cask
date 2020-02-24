using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Cask.Components
{
    public class Map : ComponentDecorator
    {
        private readonly string _mapTitle;
        private TiledMap _map;
        private TiledMapRenderer _mapRenderer;
        
        public Map(IComponent baseComponent, string mapTitle) : base(baseComponent, typeof(Transform))
        {
            _mapTitle = mapTitle;
            Layer = 0;
        }

        public override bool LoadContent(ContentManager contentManager)
        {
            _map = contentManager.Load<TiledMap>(_mapTitle);

            IsLoaded = _map != null;
            IsLoaded &= base.LoadContent(contentManager);
            
            return IsLoaded;
        }

        public override void Update(GameTime gameTime)
        {
            if(_mapRenderer != null)
                _mapRenderer.Update(gameTime);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, Camera camera)
        {
            _mapRenderer ??= new TiledMapRenderer(camera.GraphicsDevice);
            _mapRenderer.Draw(_map.GetLayer(""), camera.GetViewMatrix(), Matrix.Identity);
            
            base.Draw(gameTime, camera);
        }
    }
}