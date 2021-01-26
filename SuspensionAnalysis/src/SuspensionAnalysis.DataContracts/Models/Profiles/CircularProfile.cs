using System;

namespace SuspensionAnalysis.DataContracts.Models.Profiles
{
    public class CircularProfile : Profile
    {
        public double Diameter { get; set; }

        public override double Area
            => this.Thickness.HasValue ?
            Math.PI / 4 * (Math.Pow(this.Diameter, 2) - Math.Pow(this.Diameter - 2 * this.Thickness.Value, 2))
            : Math.PI / 4 * Math.Pow(this.Diameter, 2);

        public override double MomentOfInertia
            => this.Thickness.HasValue ?
            Math.PI / 64 * (Math.Pow(this.Diameter, 4) - Math.Pow(this.Diameter - 2 * this.Thickness.Value, 4))
            : Math.PI / 64 * Math.Pow(this.Diameter, 4);
    }
}
