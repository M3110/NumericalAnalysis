using System;

namespace SuspensionAnalysis.DataContracts.Models.Profiles
{
    public class Rectangularthis : Profile
    {
        public double Width { get; set; }

        public double Height { get; set; }

        public override double Area
            => this.Thickness.HasValue ?
            this.Width * this.Height - (this.Width - 2 * this.Thickness.Value) * (this.Height - 2 * this.Thickness.Value)
            : this.Width * this.Height;

        public override double MomentOfInertia
            => this.Thickness.HasValue ?
            (Math.Pow(this.Height, 3) * this.Width - Math.Pow(this.Height - 2 * this.Thickness.Value, 3) * (this.Width - 2 * this.Thickness.Value)) / 12
            : Math.Pow(this.Height, 3) * this.Width / 12;
    }
}
