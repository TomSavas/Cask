using Microsoft.Xna.Framework;

namespace Cask.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.X, vector3.Y);
        }

        public static Quaternion ToQuaternion(this Vector3 vector3)
        {
            return Quaternion.CreateFromYawPitchRoll(vector3.X, vector3.Y, vector3.Z);
        }

        public static Matrix ToMatrix(this Vector3 vector3)
        {
            return Matrix.CreateFromYawPitchRoll(vector3.X, vector3.Y, vector3.Z);
        }
    }
}