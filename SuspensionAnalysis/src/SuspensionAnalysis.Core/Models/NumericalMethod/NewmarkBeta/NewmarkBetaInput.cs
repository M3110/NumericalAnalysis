namespace SuspensionAnalysis.Core.Models.NumericalMethod.NewmarkBeta
{
    /// <summary>
    /// It contains the input data for Newmark-Beta method.
    /// </summary>
    public class NewmarkBetaInput : NumericalMethodInput
    {
        public override double Gama => (double)1 / 2;

        public override double Beta => (double)1 / 6;
    }
}
