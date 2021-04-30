namespace SuspensionAnalysis.DataContracts.Models.SuspensionComponents.SteeringKnuckle
{
    /// <summary>
    /// It represents the steering knuckle.
    /// </summary>
    public class SteeringKnucklePoint
    {
        /// <summary>
        /// The point of fastening with A-arm upper. 
        /// </summary>
        /// <example>x,y,z</example>
        public string AArmUpperPoint { get; set; }

        /// <summary>
        /// The point of fastening with A-arm lower.
        /// </summary>
        /// <example>x,y,z</example>
        public string AArmLowerPoint { get; set; }

        /// <summary>
        /// The point of fastening with tie rod.
        /// </summary>
        /// <example>x,y,z</example>
        public string TieRodPoint { get; set; }

        /// <summary>
        /// The point of fastening with the brake caliper support.
        /// </summary>
        public BrakeCaliperSupportPoint BrakeCaliperSupportPoint { get; set; }
    }
}
