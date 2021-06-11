using System;

namespace SuspensionAnalysis.Core.Models.NumericalMethod.Newmark
{
    /// <summary>
    /// It contains the input data for Newmark method.
    /// </summary>
    public class NewmarkMethodInput : NumericalMethodInput
    {
        public override double Gama => (double)1 / 2;

        public override double Beta => (double)1 / 6;

        #region Integration Constants

        public double A0 => 1 / (this.Beta * Math.Pow(this.TimeStep, 2));

        public double A1 => this.Gama / (this.Beta * this.TimeStep);

        public double A2 => 1 / (this.Beta * this.TimeStep);

        public double A3 => 1 / (2 * this.Beta) - 1;

        public double A4 => this.Gama / this.Beta - 1;

        public double A5 => this.TimeStep / 2 * (this.Gama / this.Beta - 2);

        public double A6 => this.TimeStep * (1 - this.Gama);

        public double A7 => this.Gama * this.TimeStep; 

        #endregion
    }
}
