using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.Models.NumericalMethod;
using SuspensionAnalysis.Core.Models.NumericalMethod.Newmark;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.NumericalMethods.DifferentialEquation.Newmark
{
    /// <summary>
    /// It is responsible to execute the Newmark numerical method to solve Differential Equation.
    /// </summary>
    public class NewmarkMethod : DifferentialEquationMethod<NewmarkMethodInput>, INewmarkMethod
    {
        /// <summary>
        /// Asynchronously, this method calculates the results for a numeric analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousResult"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override async Task<NumericalMethodResult> CalculateResult(NewmarkMethodInput input, NumericalMethodResult previousResult, double time)
        {
            if (time < 0)
                throw new ArgumentOutOfRangeException(nameof(time), "The time cannot be negative.");

            if (time == 0)
                return await this.CalculateInitialResult(input).ConfigureAwait(false);

            #region Step 1 - Asynchronously, calculates the equivalent stiffness and equivalent force.
            var tasks = new List<Task>();

            double[,] inversedEquivalentStiffness = null;
            tasks.Add(Task.Run(() => inversedEquivalentStiffness = this.CalculateEquivalentStiffness(input).InverseMatrix()));

            double[] equivalentForce = null;
            tasks.Add(Task.Run(async () => equivalentForce = await this.CalculateEquivalentForceAsync(input, previousResult.Displacement, previousResult.Velocity, previousResult.Acceleration)));

            await Task.WhenAll(tasks).ConfigureAwait(false);
            #endregion

            #region Step 2 - Calculates the displacement.
            double[] displacement = inversedEquivalentStiffness.Multiply(equivalentForce);
            #endregion

            #region Step 3 - Calculates the velocity and acceleration.
            double[] velocity = new double[input.NumberOfBoundaryConditions];
            double[] acceleration = new double[input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                acceleration[i] = input.A0 * (displacement[i] - previousResult.Displacement[i]) - input.A2 * previousResult.Velocity[i] - input.A3 * previousResult.Acceleration[i];
                velocity[i] = previousResult.Velocity[i] + input.A6 * previousResult.Acceleration[i] + input.A7 * acceleration[i];
            }
            #endregion

            return new NumericalMethodResult
            {
                Time = time,
                Displacement = displacement,
                Velocity = velocity,
                Acceleration = acceleration,
                EquivalentForce = input.EquivalentForce
            };
        }

        /// <summary>
        /// Calculates the equivalent stiffness to calculate the displacement in Newmark method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double[,] CalculateEquivalentStiffness(NewmarkMethodInput input)
        {
            double[,] equivalentStiffness = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentStiffness[i, j] = input.A0 * input.Mass[i, j] + input.A1 * input.Damping[i, j] + input.Stiffness[i, j];
                }
            }

            return equivalentStiffness;
        }

        /// <summary>
        /// Asynchronously, this method calculates the equivalent force to calculate the displacement to Newmark method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousDisplacement"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="previousAcceleration"></param>
        /// <returns></returns>
        private async Task<double[]> CalculateEquivalentForceAsync(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            #region Calculates the equivalent velocity and equivalent acceleration.
            var tasks = new List<Task>();

            double[] equivalentVelocity = null;
            tasks.Add(Task.Run(() => equivalentVelocity = this.CalculateEquivalentVelocity(input, previousDisplacement, previousVelocity, previousAcceleration)));

            double[] equivalentAcceleration = null;
            tasks.Add(Task.Run(() => equivalentAcceleration = this.CalculateEquivalentAcceleration(input, previousDisplacement, previousVelocity, previousAcceleration)));

            await Task.WhenAll(tasks).ConfigureAwait(false);
            #endregion

            #region Calculates the equivalent forces.
            var forceTasks = new List<Task>();

            double[] equivalentDampingForce = null;
            tasks.Add(Task.Run(() => equivalentDampingForce = input.Damping.Multiply(equivalentVelocity)));

            double[] equivalentDynamicForce = null;
            tasks.Add(Task.Run(() => equivalentDynamicForce = input.Mass.Multiply(equivalentAcceleration)));

            await Task.WhenAll(forceTasks).ConfigureAwait(false);
            #endregion

            return input.EquivalentForce.Sum(equivalentDampingForce, equivalentDynamicForce);
        }

        /// <summary>
        /// Calculates the equivalent aceleration to calculate the equivalent force.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousDisplacement"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="previousAcceleration"></param>
        /// <returns></returns>
        private double[] CalculateEquivalentAcceleration(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            double[] equivalentAcceleration = new double[input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                equivalentAcceleration[i] = input.A0 * previousDisplacement[i] + input.A2 * previousVelocity[i] + input.A3 * previousAcceleration[i];
            }

            return equivalentAcceleration;
        }

        /// <summary>
        /// Calculates the equivalent velocity to calculate the equivalent force.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousDisplacement"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="previousAcceleration"></param>
        /// <returns></returns>
        private double[] CalculateEquivalentVelocity(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            double[] equivalentVelocity = new double[input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                equivalentVelocity[i] = input.A1 * previousDisplacement[i] + input.A4 * previousVelocity[i] + input.A5 * previousAcceleration[i];
            }

            return equivalentVelocity;
        }
    }
}
