namespace SuspensionAnalysis.Core.Models.NumericalMethod
{
    /// <summary>
    /// It contains the input data for a numerical method.
    /// </summary>
    public abstract class NumericalMethodInput
    {
        public double TimeStep { get; set; }

        public double[,] Mass { get; set; }

        public double[,] Stiffness { get; set; }

        public double[,] Damping { get; set; }

        public double[] EquivalentForce { get; set; }

        public uint NumberOfBoundaryConditions { get; set; }

        public abstract double Gama { get; }

        public abstract double Beta { get; }
    }
}
