using Cask.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Cask.Components
{
    public class Text : ComponentDecorator
    {
        public string TextContent { get; set; }
        public Color Color { get; set; } = Color.White;
        public SpriteFont Font { get; private set; }
        public bool IsLoaded { get; private set; }

        private string _fontTitle;
        
        public Text(IComponent baseComponent, string fontTitle, string textContent = "DEFAULT TEXT", uint layer = 0,
            bool enabled = true) : base(baseComponent, typeof(Transform))
        {
            _fontTitle = fontTitle;
            TextContent = textContent;
            Layer = layer;
            Enabled = enabled;
        }
        
        public override bool LoadContent(ContentManager contentManager)
        {
            Font = contentManager.Load<SpriteFont>(_fontTitle);
            
            IsLoaded = Font != null;
            IsLoaded &= base.LoadContent(contentManager);

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
                    Dependencies.GetComponent<Transform>().Position.ToVector2(),
                    Color,
                    Dependencies.GetComponent<Transform>().Rotation.ToEulerAngles().Z,
                    Vector2.Zero, 
                    Dependencies.GetComponent<Transform>().Scale.ToVector2(), 
                    SpriteEffects.None, 
                    Layer);
                
                spriteBatch.End();
            }
        }
    }
}
