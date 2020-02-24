using Cask.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Cask.Components
{
    public class Camera : ComponentDecorator
    {
        public ViewportAdapter ViewportAdapter;
        public GraphicsDevice GraphicsDevice => ViewportAdapter.GraphicsDevice;
        
        private OrthographicCamera _camera2D;
        
        public Camera(IComponent baseComponent, ViewportAdapter viewportAdapter) : base(baseComponent, typeof(Transform))
        {
            ViewportAdapter = viewportAdapter;
            _camera2D = new OrthographicCamera(viewportAdapter);
        }

        public override void Update(GameTime gameTime)
        {
            _camera2D.Position = Dependencies.GetComponent<Transform>().Position.ToVector2();
            _camera2D.Rotation = Dependencies.GetComponent<Transform>().Rotation.ToEulerAngles().Z;
            //Dependencies.Get<Transform>().Scale = new Vector3((float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 0.2 + 1),
            //    (float)(Math.Cos(gameTime.TotalGameTime.TotalSeconds) * 0.2 + 1), 1f);//(float) (Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 0.3 + 1);
            base.Update(gameTime);
        }

        public Matrix GetInverseViewMatrix() => Matrix.Invert(GetViewMatrix());
        public BoundingFrustum GetBoundingFrustum() => _camera2D.GetBoundingFrustum();
        public ContainmentType Contains(Point point) => _camera2D.Contains(point);
        public ContainmentType Contains(Vector2 vector2) => _camera2D.Contains(vector2);
        public ContainmentType Contains(Rectangle rectangle) => _camera2D.Contains(rectangle);
        
        public Matrix GetViewMatrix() => GetViewMatrix(Vector2.One);
        
        public Matrix GetViewMatrix(Vector2 parallaxFactor)
        {
            var transform = Dependencies.GetComponent<Transform>();

            return Matrix.CreateTranslation(new Vector3(
                       -transform.Position.ToVector2() * parallaxFactor, 0.0f)) *
                   Matrix.CreateTranslation(new Vector3(-_camera2D.Origin, 0.0f)) * 
                   Matrix.CreateRotationZ(transform.Rotation.ToEulerAngles().Z) *
                   Matrix.CreateScale(transform.Scale) *
                   Matrix.CreateTranslation(new Vector3(_camera2D.Origin, 0.0f));
        }

        public Vector2 ScreenToWorld(float x, float y) => _camera2D.ScreenToWorld(new Vector2(x, y));
        
        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, GetInverseViewMatrix());
        } 
        
        public Vector2 WorldToScreen(float x, float y) => _camera2D.WorldToScreen(x, y);

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, GetViewMatrix());
        }
    }
}