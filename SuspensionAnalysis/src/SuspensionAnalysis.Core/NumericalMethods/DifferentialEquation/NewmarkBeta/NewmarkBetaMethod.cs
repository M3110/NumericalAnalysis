using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.Models.NumericalMethod;
using SuspensionAnalysis.Core.Models.NumericalMethod.NewmarkBeta;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.NumericalMethods.DifferentialEquation.NewmarkBeta
{
    /// <summary>
    /// It is responsible to execute the Newmark-Beta numerical method to solve Differential Equation.
    /// </summary>
    public class NewmarkBetaMethod : DifferentialEquationMethod<NewmarkBetaMethodInput>, INewmarkBetaMethod
    {
        /// <summary>
        /// Asynchronously, this method calculates the results for a numeric analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousResult"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override async Task<NumericalMethodResult> CalculateResult(NewmarkBetaMethodInput input, NumericalMethodResult previousResult, double time)
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
            tasks.Add(Task.Run(async () => equivalentForce = await this.CalculateEquivalentForceAsync(input, previousResult, time)));

            await Task.WhenAll(tasks).ConfigureAwait(false);
            #endregion

            #region Step 2 - Calculates the delta displacement.
            double[] deltaDisplacement = inversedEquivalentStiffness.Multiply(equivalentForce);
            #endregion

            #region Step 3 - Calculates the delta velocity and delta acceleration.
            double[] deltaVelocity = new double[input.NumberOfBoundaryConditions];
            double[] deltaAcceleration = new double[input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                deltaAcceleration[i] = input.A0 * deltaDisplacement[i] - input.A2 * previousResult.Velocity[i] - input.A4 * previousResult.Acceleration[i];
                deltaVelocity[i] = input.A1 * deltaDisplacement[i] - input.A3 * previousResult.Velocity[i] - input.A5 * previousResult.Acceleration[i];
            }
            #endregion

            #region Step 4 - Calculates the displacement, velocity and acceleration.
            var resultTasks = new List<Task>();

            double[] displacement = null;
            resultTasks.Add(Task.Run(() => displacement = previousResult.Displacement.Sum(deltaDisplacement)));

            double[] velocity = null;
            resultTasks.Add(Task.Run(() => velocity = previousResult.Velocity.Sum(deltaVelocity)));

            double[] acceleration = null;
            resultTasks.Add(Task.Run(() => acceleration = previousResult.Acceleration.Sum(deltaAcceleration)));

            await Task.WhenAll(resultTasks).ConfigureAwait(false);
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
        /// This method calculates the equivalent stiffness to calculate the displacement in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double[,] CalculateEquivalentStiffness(NewmarkBetaMethodInput input)
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
        /// Asynchronously, this method calculates the equivalent force to calculate the displacement in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousResult"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private async Task<double[]> CalculateEquivalentForceAsync(NewmarkBetaMethodInput input, NumericalMethodResult previousResult, double time)
        {
            #region Calculates the equivalent damping and equivalent mass.
            var tasks = new List<Task>();

            double[,] equivalentDamping = null;
            tasks.Add(Task.Run(() => equivalentDamping = this.CalculateEquivalentDamping(input)));

            double[,] equivalentMass = null;
            tasks.Add(Task.Run(() => equivalentMass = this.CalculateEquivalentMass(input)));

            await Task.WhenAll(tasks).ConfigureAwait(false);
            #endregion

            #region Calculates the equivalent forces.
            var forceTasks = new List<Task>();

            double[] equivalentDampingForce = null;
            tasks.Add(Task.Run(() => equivalentDampingForce = equivalentDamping.Multiply(previousResult.Velocity)));

            double[] equivalentDynamicForce = null;
            tasks.Add(Task.Run(() => equivalentDynamicForce = equivalentMass.Multiply(previousResult.Acceleration)));

            await Task.WhenAll(forceTasks).ConfigureAwait(false);
            #endregion

            return input.EquivalentForce
                .Subtract(previousResult.EquivalentForce)
                .Sum(equivalentDampingForce, equivalentDynamicForce);
        }

        /// <summary>
        /// This method calculates the equivalent damping to be used in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double[,] CalculateEquivalentDamping(NewmarkBetaMethodInput input)
        {
            double[,] equivalentDamping = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentDamping[i, j] = input.A2 * input.Mass[i, j] + input.A3 * input.Damping[i, j];
                }
            }

            return equivalentDamping;
        }

        /// <summary>
        /// This method calculates the equivalent mass to be used in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double[,] CalculateEquivalentMass(NewmarkBetaMethodInput input)
        {
            double[,] equivalentMass = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentMass[i, j] = input.A4 * input.Mass[i, j] + input.A5 * input.Damping[i, j];
                }
            }

            return equivalentMass;
        }
    }
}
