using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.Models.NumericalMethod;
using SuspensionAnalysis.Core.Models.NumericalMethod.NewmarkBeta;
using System;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.NumericalMethods.DifferentialEquation.NewmarkBeta
{
    /// <summary>
    /// It is responsible to execute the Newmark-Beta numerical method to solve Differential Equation.
    /// </summary>
    public class NewmarkBetaMethod : DifferentialEquationMethod<NewmarkBetaInput>, INewmarkBetaMethod
    {
        /// <summary>
        /// Calculates and write in a file the results for a one degree of freedom analysis using Newmark-Beta integration method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousResult"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override Task<NumericalMethodResult> CalculateResult(NewmarkBetaInput input, NumericalMethodResult previousResult, double time)
        {
            double[,] equivalentStiffness = CalculateEquivalentStiffness(input);
            double[,] inversedEquivalentStiffness = equivalentStiffness.InverseMatrix();

            double[] equivalentForce = CalculateEquivalentForce(input, previousResult, time);

            double[] deltaDisplacement = inversedEquivalentStiffness.Multiply(equivalentForce);

            double[] deltaVelocity = new double[input.NumberOfBoundaryConditions];
            double[] deltaAcceleration = new double[input.NumberOfBoundaryConditions];

            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                deltaVelocity[i] = (input.Gama / (input.Beta * input.TimeStep)) * deltaDisplacement[i] - (input.Gama / input.Beta) * previousResult.Velocity[i] + input.TimeStep * (1 - input.Gama / (2 * input.Beta)) * previousResult.Acceleration[i];
                deltaAcceleration[i] = 1 / (input.Beta * Math.Pow(input.TimeStep, 2)) * deltaDisplacement[i] - 1 / (input.Beta * input.TimeStep) * previousResult.Velocity[i] - 1 / (2 * input.Beta) * previousResult.Acceleration[i];
            }

            return Task.FromResult(new NumericalMethodResult
            {
                Displacement = previousResult.Displacement.Sum(deltaDisplacement),
                Velocity = previousResult.Velocity.Sum(deltaVelocity),
                Acceleration = previousResult.Acceleration.Sum(deltaAcceleration),
                EquivalentForce = input.EquivalentForce
            });
        }

        /// <summary>
        /// Calculates the equivalent stiffness to calculate the displacement in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double[,] CalculateEquivalentStiffness(NewmarkBetaInput input)
        {
            double[,] equivalentStiffness = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];

            double const1 = 1 / (input.Beta * Math.Pow(input.TimeStep, 2));
            double const2 = input.Gama / (input.Beta * input.TimeStep);

            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentStiffness[i, j] = const1 * input.Mass[i, j] + const2 * input.Damping[i, j] + input.Stiffness[i, j];
                }
            }

            return equivalentStiffness;
        }

        /// <summary>
        /// Calculates the equivalent damping to be used in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double[,] CalculateEquivalentDamping(NewmarkBetaInput input)
        {
            double[,] equivalentDamping = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];

            double const1 = 1 / (input.Beta * input.TimeStep);
            double const2 = input.Gama / input.Beta;

            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentDamping[i, j] = const1 * input.Mass[i, j] + const2 * input.Damping[i, j];
                }
            }

            return equivalentDamping;
        }

        /// <summary>
        /// Calculates the equivalent mass to be used in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double[,] CalculateEquivalentMass(NewmarkBetaInput input)
        {
            double[,] equivalentMass = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];

            double const1 = 1 / (2 * input.Beta);
            double const2 = -input.TimeStep * (1 - input.Gama / (2 * input.Beta));

            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentMass[i, j] = const1 * input.Mass[i, j] + const2 * input.Damping[i, j];
                }
            }

            return equivalentMass;
        }

        /// <summary>
        /// Calculates the equivalent force to calculate the displacement in Newmark-Beta method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousResult"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double[] CalculateEquivalentForce(NewmarkBetaInput input, NumericalMethodResult previousResult, double time)
        {
            double[,] equivalentDamping = CalculateEquivalentDamping(input);
            double[,] equivalentMass = CalculateEquivalentMass(input);

            double[] dampingVelocity = equivalentDamping.Multiply(previousResult.Velocity);
            double[] massAcceleration = equivalentMass.Multiply(previousResult.Acceleration);

            double[] deltaForce = input.EquivalentForce.Subtract(previousResult.EquivalentForce);

            double[] equivalentForce = deltaForce.Sum(dampingVelocity, massAcceleration);

            return equivalentForce;
        }
    }
}
