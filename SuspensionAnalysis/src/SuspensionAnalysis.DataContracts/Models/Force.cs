namespace SuspensionAnalysis.DataContracts.Models
{
    /// <summary>
    /// It represents the force.
    /// </summary>
    public class Force
    {
        /// <summary>
        /// the absolut value to force.
        /// </summary>
        public double AbsolutValue { get; set; }

        /// <summary>
        /// The force at axis X.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The force at axis Y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The force at axis Z.
        /// </summary>
        public double Z { get; set; }
    }
}
