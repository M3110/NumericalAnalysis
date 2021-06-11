using SuspensionAnalysis.Core.Models.NumericalMethod;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.NumericalMethods.DifferentialEquation
{
    /// <summary>
    /// It is responsible to execute numerical method to solve Differential Equation.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public abstract class DifferentialEquationMethod<TInput> : IDifferentialEquationMethod<TInput>
        where TInput : NumericalMethodInput
    {
        /// <summary>
        /// Asynchronously, this method calculates the result for the initial time for a matricial analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual Task<NumericalMethodResult> CalculateInitialResult(TInput input)
        {
            return Task.FromResult(new NumericalMethodResult
            {
                Displacement = new double[input.NumberOfBoundaryConditions],
                Velocity = new double[input.NumberOfBoundaryConditions],
                Acceleration = new double[input.NumberOfBoundaryConditions],
                EquivalentForce = input.EquivalentForce
            });
        }

        /// <summary>
        /// Asynchronously, this method calculates the results for a numeric analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousResult"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public abstract Task<NumericalMethodResult> CalculateResult(TInput input, NumericalMethodResult previousResult, double time);
    }
}
