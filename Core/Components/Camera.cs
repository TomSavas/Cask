using System;
using System.Collections.Generic;
using System.ComponentModel;
using Core.Extensions;
using Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Components
{
    public class Camera : IComponent
    {
        public bool Enabled { get; set; }
        public ICollection<Type> RequiredComponents { get; } = new List<Type> {typeof(Transform)};
        public IComponentDependencies Dependencies { get; } = new ComponentDependencies();
        
        public GraphicsDeviceManager GraphicsDeviceManager { get; }
        public bool IsPerspecive { get; set; }
        public float FOV { get; set; } = MathHelper.PiOver4;
        public float NearClipPlane { get; set; } = 1;
        public float FarClipPlane { get; set; } = 100;
        public float AspectRatio => GraphicsDeviceManager.GraphicsDevice.Viewport.AspectRatio;
        public float Width => GraphicsDeviceManager.GraphicsDevice.Viewport.Width;
        public float Height => GraphicsDeviceManager.GraphicsDevice.Viewport.Height;
        public bool IsLoaded => true;
        
        public Matrix ViewMatrix
        {
            get
            {
                return Matrix.CreateLookAt(
                    Dependencies.Get<Transform>().Position, 
                    Vector3.Zero, 
                    Vector3.Up);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                if (IsPerspecive)
                    return Matrix.CreatePerspectiveFieldOfView(FOV, AspectRatio, NearClipPlane, FarClipPlane);
                else
                    return Matrix.CreateOrthographic(Width, Height, NearClipPlane, FarClipPlane);
            }
        }

        public Camera(GraphicsDeviceManager graphicsDeviceManager, bool isPerspective = true)
        {
            GraphicsDeviceManager = graphicsDeviceManager;
            isPerspective = isPerspective;
        }

        public Vector2 ScreenPointToWorldPoint(Vector2 screenPoint)
        {
            return screenPoint + Dependencies.Get<Transform>().Position.ToVector2();
        }

        public Vector3 ScreenPointToWorldPoint(Vector3 screenPoint)
        {
            //TODO: Might want some more intricate raycasting later.
            return new Vector3(ScreenPointToWorldPoint(screenPoint.ToVector2()), 0);
        }

        public bool LoadContent(ContentManager contentManager) => true;
        public void Update(GameTime gameTime) {}
    }
}