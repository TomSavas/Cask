using System;
using System.Collections.Generic;
using Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Components
{
    public class Sprite : IDrawable
    {
        public bool Enabled { get; set; }
        public uint Layer { get; set; }
        public bool IsVisible { get; set; }

        public ICollection<Type> RequiredComponents { get; } = new List<Type> {typeof(Transform)};
        public IComponentDependencies Dependencies { get; } = new ComponentDependencies();
        public bool IsLoaded { get; private set; }

        private string _textureTitle;
        private Texture2D _spriteTexture2D;

        public Sprite(string textureTitle, uint layer = 0, bool enabled = true)
        {
            _textureTitle = textureTitle;
            Layer = layer;
            Enabled = enabled;
        }
        
        public void Update(GameTime gameTime) {}

        public bool LoadContent(ContentManager contentManager)
        {
            _spriteTexture2D = contentManager.Load<Texture2D>(_textureTitle);
            IsLoaded = true;

            return IsLoaded;
        }

        public void Draw(GameTime gameTime, Camera camera)
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
            }
        }
    }
}