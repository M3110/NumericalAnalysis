using SuspensionAnalysis.Infrastructure.Models;

namespace SuspensionAnalysis.Core.Models
{
    public class SuspensionNormalizedVector
    {
        public Vector3D SuspensionAArmLowerU1 { get; set; }

        public Vector3D SuspensionAArmLowerU2 { get; set; }

        public Vector3D SuspensionAArmUpperU3 { get; set; }

        public Vector3D SuspensionAArmUpperU4 { get; set; }

        public Vector3D ShockAbsorberU5 { get; set; }

        public Vector3D TieRodU6 { get; set; }
    }
}
