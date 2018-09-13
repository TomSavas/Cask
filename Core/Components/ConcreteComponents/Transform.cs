using Microsoft.Xna.Framework;

namespace Core.Components
{
    public class Transform : ComponentDecorator
    {
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
        
        public Transform(IComponent baseComponent, bool enabled = true)
            : this(baseComponent, Vector3.Zero, enabled) {}

        public Transform(IComponent baseComponent, Vector3 position, bool enabled = false)
            : this(baseComponent, position, Vector3.One, enabled) {}

        public Transform(IComponent baseComponent, Vector3 position, Vector3 scale, bool enabled = true)
            : this(baseComponent, position, scale, Matrix.Identity, enabled) {}

        public Transform(IComponent baseComponent, Vector3 position, Matrix rotation, bool enabled = true)
            : this(baseComponent, position, Vector3.One, rotation, enabled) {}
        
        public Transform(IComponent baseComponent, Vector3 position, Vector3 scale, Matrix rotation, bool enabled = true) : base(baseComponent)
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
    }
}