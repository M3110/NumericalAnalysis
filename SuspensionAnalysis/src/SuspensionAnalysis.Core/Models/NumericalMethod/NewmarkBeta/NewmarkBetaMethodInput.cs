using System;

namespace SuspensionAnalysis.Core.Models.NumericalMethod.NewmarkBeta
{
    /// <summary>
    /// It contains the input data for Newmark-Beta method.
    /// </summary>
    public class NewmarkBetaMethodInput : NumericalMethodInput
    {
        public override double Gama => (double)1 / 2;

        public override double Beta => (double)1 / 6;

        #region Integration Constants

        public double A0 => 1 / (this.Beta * Math.Pow(this.TimeStep, 2));

        public double A1 => this.Gama / (this.Beta * this.TimeStep);

        public double A2 => 1 / (this.Beta * this.TimeStep);

        public double A3 => this.Gama / this.Beta;

        public double A4 => 1 / (2 * this.Beta);

        public double A5 => -this.TimeStep * (1 - this.Gama / (2 * this.Beta));

        #endregion
    }
}
