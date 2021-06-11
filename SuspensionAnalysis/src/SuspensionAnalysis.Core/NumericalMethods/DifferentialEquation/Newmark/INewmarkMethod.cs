using SuspensionAnalysis.Core.Models.NumericalMethod.Newmark;

namespace SuspensionAnalysis.Core.NumericalMethods.DifferentialEquation.Newmark
{
    /// <summary>
    /// It is responsible to execute the Newmark numerical method to solve Differential Equation.
    /// </summary>
    public interface INewmarkMethod : IDifferentialEquationMethod<NewmarkMethodInput> { }
}
