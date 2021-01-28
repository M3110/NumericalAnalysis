using SuspensionAnalysis.Infrastructure.Models;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    public static class Vector3DExtension
    {
        public static Vector3D NormalizeVector(this Vector3D vector)
            => new Vector3D
            {
                X = vector.X / vector.Length,
                Y = vector.Y / vector.Length,
                Z = vector.Z / vector.Length
            };

        public static Vector3D CrossProduct(this Vector3D vector1, Vector3D vector2)
            => new Vector3D
            {
                X = vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                Y = vector1.Z * vector2.X - vector1.X * vector2.Z,
                Z = vector1.X * vector2.Y - vector1.Y * vector2.X
            };

        public static double DotProduct(this Vector3D vector1, Vector3D vector2)
            => vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
    }
}
