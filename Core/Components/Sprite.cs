using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Components
{
    public class Sprite : ComponentDecorator
    {
        private string _textureTitle;
        private Texture2D _spriteTexture2D;

        public Sprite(IComponent baseComponent, string textureTitle, uint layer = 0, bool enabled = true) : base(baseComponent)
        {
            RequiredComponents = new ReadOnlyCollection<Type>(new List<Type>{typeof(Transform)});
            _textureTitle = textureTitle;
            Layer = layer;
            Enabled = enabled;
        }
        
        public override bool LoadContent(ContentManager contentManager)
        {
            _spriteTexture2D = contentManager.Load<Texture2D>(_textureTitle);
            IsLoaded = true && base.LoadContent(contentManager);

            return IsLoaded;
        }

        public override void Draw(GameTime gameTime, Camera camera)
        {
            if (Enabled)
            {
                var spriteBatch = new SpriteBatch(camera.GraphicsDeviceManager.GraphicsDevice);

                var cameraTransform = camera.Dependencies.Get<Transform>();
                var cameraTransformMatrix = Matrix.Identity*
                            Matrix.CreateTranslation(-cameraTransform.Position.X, -cameraTransform.Position.Y, 0)*
                            Matrix.CreateRotationZ(cameraTransform.Rotation.ToEulerAngles().Z)*
                            Matrix.CreateTranslation(0, 0, 0);
                
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cameraTransformMatrix);
                spriteBatch.Draw(_spriteTexture2D,
                    new Rectangle((int) Dependencies.Get<Transform>().Position.ToVector2().X,
                        (int) Dependencies.Get<Transform>().Position.ToVector2().Y,
                        (int) (_spriteTexture2D.Width * Dependencies.Get<Transform>().Scale.ToVector2().X),
                        (int) (_spriteTexture2D.Width * Dependencies.Get<Transform>().Scale.ToVector2().Y)),
                    null,
                    Color.White,
                    Dependencies.Get<Transform>().Rotation.ToEulerAngles().Z,
                    Vector2.Zero, 
                    SpriteEffects.None,
                    0f);
                
                spriteBatch.End();
                
                base.Draw(gameTime, camera);
            }
        }
    }
}