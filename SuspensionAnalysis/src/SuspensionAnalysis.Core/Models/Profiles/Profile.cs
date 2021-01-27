namespace SuspensionAnalysis.Infraestructure.Models.Profiles
{
    public abstract class Profile
    {
        public double? Thickness { get; set; }

        public abstract double Area { get; }

        public abstract double MomentOfInertia { get; }
    }
}
