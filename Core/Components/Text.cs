using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Components
{
    public class Text : ComponentDecorator
    {
        public string TextContent { get; set; }
        public Color Color { get; set; } = Color.White;
        public SpriteFont Font { get; private set; }
        public bool IsLoaded { get; private set; }

        private string _fontTitle;
        
        public Text(IComponent baseComponent, string fontTitle, string textContent = "DEFAULT TEXT", uint layer = 0, bool enabled = true) : base(baseComponent)
        {
            RequiredComponents = new ReadOnlyCollection<Type>(new List<Type>{typeof(Transform)});
            _fontTitle = fontTitle;
            TextContent = textContent;
            Layer = layer;
            Enabled = enabled;
        }
        
        public override bool LoadContent(ContentManager contentManager)
        {
            Font = contentManager.Load<SpriteFont>(_fontTitle);
            IsLoaded = base.LoadContent(contentManager);

            return IsLoaded;
        }

        public override void Draw(GameTime gameTime, Camera camera)
        {
            if (Enabled)
            {
                var spriteBatch = new SpriteBatch(camera.GraphicsDevice);
                
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix());
                spriteBatch.DrawString(Font,
                    TextContent,
                    Dependencies.Get<Transform>().Position.ToVector2(),
                    Color,
                    Dependencies.Get<Transform>().Rotation.ToEulerAngles().Z,
                    Vector2.Zero, 
                    Dependencies.Get<Transform>().Scale.ToVector2(), 
                    SpriteEffects.None, 
                    Layer);
                
                spriteBatch.End();
            }
        }
    }
}
