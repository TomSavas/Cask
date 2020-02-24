using Cask.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Cask.Components
{
    public class Sprite : ComponentDecorator
    {
        private string _textureTitle;
        private Texture2D _spriteTexture2D;

        public Sprite(IComponent baseComponent, string textureTitle, uint layer = 0, bool enabled = true) : base(baseComponent, typeof(Transform))
        {
            _textureTitle = textureTitle;
            Layer = layer;
            Enabled = enabled;
        }
        
        public override bool LoadContent(ContentManager contentManager)
        {
            _spriteTexture2D = contentManager.Load<Texture2D>(_textureTitle);
            
            IsLoaded = _spriteTexture2D != null;
            IsLoaded &= base.LoadContent(contentManager);

            return IsLoaded;
        }

        public override void Draw(GameTime gameTime, Camera camera)
        {
            if (Enabled)
            {
                var spriteBatch = new SpriteBatch(camera.GraphicsDevice);

                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix());
                spriteBatch.Draw(_spriteTexture2D,
                    new Rectangle((int) Dependencies.GetComponent<Transform>().Position.ToVector2().X,
                        (int) Dependencies.GetComponent<Transform>().Position.ToVector2().Y,
                        (int) (_spriteTexture2D.Width * Dependencies.GetComponent<Transform>().Scale.ToVector2().X),
                        (int) (_spriteTexture2D.Width * Dependencies.GetComponent<Transform>().Scale.ToVector2().Y)),
                    null,
                    Color.White,
                    Dependencies.GetComponent<Transform>().Rotation.ToEulerAngles().Z,
                    Vector2.Zero, 
                    SpriteEffects.None,
                    0f);
                
                spriteBatch.End();
                
                base.Draw(gameTime, camera);
            }
        }
    }
}