namespace SuspensionAnalysis.DataContracts.Models.Profiles
{
    /// <summary>
    /// It represents the rectangular profile.
    /// </summary>
    public class RectangularProfile : Profile
    {
        /// <summary>
        /// The width.
        /// Unit: m (meter).
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// The height.
        /// Unit: m (meter).
        /// </summary>
        public double Height { get; set; }
    }
}
