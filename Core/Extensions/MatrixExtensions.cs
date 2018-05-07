using System;
using Microsoft.Xna.Framework;

namespace Core.Extensions
{
    public static class MatrixExtensions
    {
        public static Vector3 ToEulerAngles(this Matrix matrix)
        {
            Vector3 euler;
            euler.X = (float) Math.Asin(-matrix.M32);
            if (Math.Cos(euler.X) > 0.0001)                 // Not at poles
            {
                euler.Y = (float) Math.Atan2(matrix.M31, matrix.M33);
                euler.Z = (float) Math.Atan2(matrix.M12, matrix.M22);
            }
            else
            {
                euler.Y = 0.0f;
                euler.Z = (float) Math.Atan2(-matrix.M21, matrix.M11);
            }

            return euler;
        }
    }
}