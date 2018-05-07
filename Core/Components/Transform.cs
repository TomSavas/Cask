using System;
using System.Collections.Generic;
using Core.Extensions;
using Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Components
{
    public class Transform : IComponent
    {
        public ICollection<Type> RequiredComponents { get; } = new List<Type>();
        public IComponentDependencies Dependencies { get; } = new ComponentDependencies();
        
        public bool Enabled { get; set; }
        public bool IsLoaded => true;

        public Vector3 Position
        {
            get
            {
                if (Dependencies.Contains<Transform>())
                {
                    var parentTransform = Dependencies.Get<Transform>();

                    return new Vector3(_position.X * parentTransform.Scale.X + parentTransform.Position.X,
                        _position.Y * parentTransform.Scale.Y + parentTransform.Position.Y,
                        _position.Z * parentTransform.Scale.Z + parentTransform.Position.Z);
                }
                else
                {
                    return _position;
                }
            }
            set
            {
                if (Dependencies.Contains<Transform>())
                {
                    var parentTransform = Dependencies.Get<Transform>();

                    _position = new Vector3(value.X - parentTransform.Position.X,
                        value.Y - parentTransform.Position.Y,
                        value.Z - parentTransform.Position.Z);
                }
                else
                {
                    _position = value;
                }
            }
        }
        public Matrix Rotation
        {
            get
            {
                if (Dependencies.Contains<Transform>())
                {
                    var parentTransform = Dependencies.Get<Transform>();
                    
                    return Matrix.Multiply(parentTransform.Rotation, _rotation);
                }
                else
                {
                    return _rotation;
                }     
            } 
            set { _rotation = value; }
        }
        public Vector3 Scale
        {
            get
            {
                if (Dependencies.Contains<Transform>())
                {
                    var parentTransform = Dependencies.Get<Transform>();
                
                    return new Vector3(
                        parentTransform.Scale.X * _scale.X,
                        parentTransform.Scale.Y * _scale.Y,
                        parentTransform.Scale.Z * _scale.Z);
                }
                else
                {
                    return _scale;
                }
            }
            set { _scale = value; }
        }

        private Vector3 _position;
        private Matrix _rotation;
        private Vector3 _scale;
        
        public Transform(bool enabled = true)
            : this(Vector3.Zero, enabled) {}

        public Transform(Vector3 position, bool enabled = true)
            : this(position, Vector3.One, enabled) {}

        public Transform(Vector3 position, Vector3 scale, bool enabled = true)
            : this(position, scale, Matrix.Identity, enabled) {}

        public Transform(Vector3 position, Matrix rotation, bool enabled = true)
            : this(position, Vector3.One, rotation, enabled) {}
        
        public Transform(Vector3 position, Vector3 scale, Matrix rotation, bool enabled = true)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Enabled = enabled;
        }

        public void SetTransformParent(Transform transform)
        {
            Dependencies.Remove<Transform>();
            Dependencies.Add<Transform>(transform);
        }

        
        public bool LoadContent(ContentManager contentManager) => true;
        public void Update(GameTime gameTime) {}
    }
}