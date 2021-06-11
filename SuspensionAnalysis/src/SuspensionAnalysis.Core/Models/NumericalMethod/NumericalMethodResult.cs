namespace SuspensionAnalysis.Core.Models.NumericalMethod
{
    /// <summary>
    /// It contains the finite element analysis results to a specific time.
    /// </summary>
    // TODO: This class must be sealed.
    public class NumericalMethodResult
    {
        public double Time { get; set; }

        /// <summary>
        /// The displacement vector.
        /// </summary>
        public double[] Displacement { get; set; }

        /// <summary>
        /// The velocity vector.
        /// </summary>
        public double[] Velocity { get; set; }

        /// <summary>
        /// The acceleration vector.
        /// </summary>
        public double[] Acceleration { get; set; }

        /// <summary>
        /// The force vector.
        /// </summary>
        public double[] EquivalentForce { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Time}," +
                $"{string.Join(",", this.Displacement)}," +
                $"{string.Join(",", this.Velocity)}," +
                $"{string.Join(",", this.Acceleration)}," +
                $"{string.Join(",", this.EquivalentForce)}";
        }
    }
}
