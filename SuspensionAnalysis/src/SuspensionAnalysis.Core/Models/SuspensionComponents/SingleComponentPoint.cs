namespace SuspensionAnalysis.Infraestructure.Models.SuspensionComponents
{
    public abstract class SingleComponentPoint
    {
        public Point3D PivotPoint { get; set; }

        public Point3D FasteningPoint { get; set; }
    }
}
