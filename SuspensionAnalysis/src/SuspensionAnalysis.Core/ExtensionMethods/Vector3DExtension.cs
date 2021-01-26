using System.Windows.Media.Media3D;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    public static class Vector3DExtension
    {
        public static Vector3D NormalizeVector(this Vector3D vector)
        {
            vector.Normalize();

            return vector;
        }
    }
}
