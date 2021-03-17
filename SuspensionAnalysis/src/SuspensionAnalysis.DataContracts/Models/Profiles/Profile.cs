namespace SuspensionAnalysis.DataContracts.Models.Profiles
{
    /// <summary>
    /// It represents the generic profile.
    /// </summary>
    public abstract class Profile
    {
        /// <summary>
        /// The thickness.
        /// Unit: m (meter).
        /// </summary>
        public double? Thickness { get; set; }
    }
}
