using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.Models.NumericalMethod;
using SuspensionAnalysis.Core.Models.NumericalMethod.Newmark;
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
        public override Task<NumericalMethodResult> CalculateResult(NewmarkMethodInput input, NumericalMethodResult previousResult, double time)
        {
            if (time == 0)
            {
                return this.CalculateInitialResult(input);
            }

            var result = new NumericalMethodResult
            {
                Time = time,
                Displacement = new double[input.NumberOfBoundaryConditions],
                Velocity = new double[input.NumberOfBoundaryConditions],
                Acceleration = new double[input.NumberOfBoundaryConditions],
                EquivalentForce = input.EquivalentForce
            };

            double[,] equivalentStiffness = this.CalculateEquivalentStiffness(input);
            double[,] inversedEquivalentStiffness = equivalentStiffness.InverseMatrix();

            double[] equivalentForce = this.CalculateEquivalentForce(input, previousResult.Displacement, previousResult.Velocity, previousResult.Acceleration);

            result.Displacement = inversedEquivalentStiffness.Multiply(equivalentForce);

            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                result.Acceleration[i] = input.A0 * (result.Displacement[i] - previousResult.Displacement[i]) - input.A2 * previousResult.Velocity[i] - input.A3 * previousResult.Acceleration[i];
                result.Velocity[i] = previousResult.Velocity[i] + input.A6 * previousResult.Acceleration[i] + input.A7 * result.Acceleration[i];
            }

            return Task.FromResult(result);
        }

        /// <summary>
        /// Calculates the equivalent force to calculate the displacement to Newmark method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousDisplacement"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="previousAcceleration"></param>
        /// <returns></returns>
        public double[] CalculateEquivalentForce(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            double[] equivalentVelocity = this.CalculateEquivalentVelocity(input, previousDisplacement, previousVelocity, previousAcceleration);
            double[] equivalentAcceleration = this.CalculateEquivalentAcceleration(input, previousDisplacement, previousVelocity, previousAcceleration);

            double[] mass_accel = input.Mass.Multiply(equivalentAcceleration);
            double[] damping_vel = input.Damping.Multiply(equivalentVelocity);

            double[] equivalentForce = new double[input.NumberOfBoundaryConditions];
            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                equivalentForce[i] = input.EquivalentForce[i] + mass_accel[i] + damping_vel[i];
            }

            return equivalentForce;
        }

        /// <summary>
        /// Calculates the equivalent aceleration to calculate the equivalent force.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousDisplacement"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="previousAcceleration"></param>
        /// <returns></returns>
        public double[] CalculateEquivalentAcceleration(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
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
        public double[] CalculateEquivalentVelocity(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            double[] equivalentVelocity = new double[input.NumberOfBoundaryConditions];

            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                equivalentVelocity[i] = input.A1 * previousDisplacement[i] + input.A4 * previousVelocity[i] + input.A5 * previousAcceleration[i];
            }

            return equivalentVelocity;
        }

        /// <summary>
        /// Calculates the equivalent stiffness to calculate the displacement in Newmark method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double[,] CalculateEquivalentStiffness(NewmarkMethodInput input)
        {
            double[,] equivalentStiffness = new double[input.NumberOfBoundaryConditions, input.NumberOfBoundaryConditions];

            for (int i = 0; i < input.NumberOfBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfBoundaryConditions; j++)
                {
                    equivalentStiffness[i, j] = input.Stiffness[i, j] + input.A0 * input.Mass[i, j] + input.A1 * input.Damping[i, j];
                }
            }

            return equivalentStiffness;
        }
    }
}
