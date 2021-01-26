using System.Windows.Media.Media3D;

namespace SuspensionAnalysis.DataContracts.Models
{
    public class SuspensionAArmPoint
    {
        public Point3D KnucklePoint { get; set; }

        public Point3D PivotPoint1 { get; set; }

        public Point3D PivotPoint2 { get; set; }
    }
}
