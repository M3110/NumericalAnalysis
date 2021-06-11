namespace SuspensionAnalysis.Core.Models.DynamicAnalysis
{
    public class DynamicModel
    {
        public double[,] Mass { get; set; }

        public double[,] Damping { get; set; }

        public double[,] Stiffness { get; set; }

        public double[] Force { get; set; }

        public double[,] BasicExcitationDamping { get; set; }

        public double[] BasicExcitationVelocitity { get; set; }

        public double[,] BasicExcitationStifness { get; set; }

        public double[] BasicExcitationDisplacement { get; set; }
    }
}
