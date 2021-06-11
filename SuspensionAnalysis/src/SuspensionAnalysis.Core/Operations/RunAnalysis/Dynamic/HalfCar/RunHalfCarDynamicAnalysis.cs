using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.Models;
using SuspensionAnalysis.Core.Models.DynamicAnalysis;
using SuspensionAnalysis.Core.Models.NumericalMethod;
using SuspensionAnalysis.Core.NumericalMethods.DifferentialEquation.Newmark;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.RunAnalysis.Dynamic.HalfCar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.Dynamic.HalfCar
{
    public class RunHalfCarDynamicAnalysis : OperationBase<RunHalfCarDynamicAnalysisRequest, RunHalfCarDynamicAnalysisResponse, RunHalfCarDynamicAnalysisResponseData>, IRunHalfCarDynamicAnalysis
    {
        private readonly uint _numberOfBoundaryConditions = 4;

        private readonly INewmarkMethod _numericalMethod;

        public RunHalfCarDynamicAnalysis(INewmarkMethod newmarkMethod)
        {
            this._numericalMethod = newmarkMethod;
        }

        public string CreateSolutionFile()
        {
            var fileInfo = new FileInfo(Path.Combine(
                BasePaths.HalfCarDynamicAnalysis,
                $"{DateTime.Now:yyyy-MM-dd}.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        public double[,] BuildMassMatrix(double mass, double momentOfInertia, double driverMass, double engineMass)
        {
            return new double[,]
            {
                { mass, 0, 0, 0 },
                { 0, momentOfInertia, 0, 0 },
                { 0, 0, driverMass, 0 },
                { 0, 0, 0, engineMass },
            };
        }

        public double[,] BuildStiffnessMatrix(double rearStiffness, double rearDistance, double frontStiffness, double frontDistance,
            double driverStiffness, double driverDistance, double engineStiffness, double engineDistance)
        {
            return new double[,]
            {
                {
                    frontStiffness + rearStiffness + driverStiffness + engineStiffness,
                    frontStiffness * frontDistance - rearStiffness * rearDistance + driverStiffness * driverDistance - engineStiffness * engineDistance,
                    -driverStiffness,
                    -engineStiffness
                },
                {
                    frontStiffness * frontDistance - rearStiffness * rearDistance + driverStiffness * driverDistance - engineStiffness * engineDistance,
                    frontStiffness * Math.Pow(frontDistance, 2) + rearStiffness * Math.Pow(rearDistance, 2) + driverStiffness * Math.Pow(driverDistance, 2) + engineStiffness * Math.Pow(engineDistance, 2),
                    -driverStiffness * driverDistance,
                    engineStiffness * engineDistance
                },
                {
                    -driverStiffness,
                    -driverStiffness * driverDistance,
                    driverStiffness,
                    0
                },
                {
                    -engineStiffness,
                    engineStiffness * engineDistance,
                    0,
                    engineStiffness
                },
            };
        }

        public double[,] BuildDampingMatrix(double rearDamping, double rearDistance, double frontDamping, double frontDistance)
        {
            return new double[,]
            {
                { 
                    frontDamping + rearDamping, 
                    frontDamping * frontDistance - rearDamping * rearDistance, 
                    0, 
                    0 
                },
                { 
                    frontDamping * frontDistance - rearDamping * rearDistance, 
                    frontDamping * Math.Pow(frontDistance, 2) + rearDamping * Math.Pow(rearDistance, 2), 
                    0, 
                    0 
                },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
        }

        public double[] BuildForceVector(double mass, double driverMass, double engineMass)
        {
            return new double[]
            {
                -mass * Constants.GravityAcceleration,
                0,
                -driverMass * Constants.GravityAcceleration,
                -engineMass * Constants.GravityAcceleration
            };
        }

        public double[,] BuildBasicExcitationDampingMatrix(double rearDamping, double rearDistance, double frontDamping, double frontDistance)
        {
            return new double[,]
            {
                { frontDamping, rearDamping, 0, 0 },
                { frontDamping * frontDistance, -rearDamping * rearDistance, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
        }

        public double[,] BuildBasicExcitationStiffnessMatrix(double rearStiffness, double rearDistance, double frontStiffness, double frontDistante)
        {
            return new double[,]
            {
                { rearStiffness, frontStiffness, 0, 0 },
                { rearStiffness * rearDistance, -frontStiffness * frontDistante, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            };
        }

        public (double[] Displacement, double[] Velocity) BuildBasicExcitationDisplacementAndVelocityVectors(
            double velocity, double length, double obstacleLength, double obstacleHeight, double obstacleDistance, double time)
        {
            double rearVelocity = 0;
            double rearDisplacement = 0;
            double frontVelocity = 0;
            double frontDisplacement = 0;

            double angularFrequency = 2 * Math.PI * velocity / obstacleLength;

            double rearInitialTime = obstacleDistance / velocity;
            double rearFinalTime = obstacleLength / velocity + rearInitialTime;
            if (rearInitialTime <= time && time >= rearFinalTime)
            {
                rearVelocity = (obstacleHeight / 2) * angularFrequency * Math.Sin(angularFrequency * (time - rearInitialTime));
                rearDisplacement = (obstacleHeight / 2) * (1 - Math.Cos(angularFrequency * (time - rearInitialTime)));
            }

            double frontInitialTime = rearInitialTime + length / velocity;
            double frontFinalTime = rearFinalTime + length / velocity;
            if (frontInitialTime <= time && time >= frontFinalTime)
            {
                frontVelocity = (obstacleHeight / 2) * angularFrequency * Math.Sin(angularFrequency * (time - frontInitialTime));
                frontDisplacement = (obstacleHeight / 2) * (1 - Math.Cos(angularFrequency * (time - frontInitialTime)));
            }

            return (new double[] { rearDisplacement, frontDisplacement, 0, 0 }, new double[] { rearVelocity, frontVelocity, 0, 0 });
        }

        public async Task<DynamicModel> BuildDynamicModelAsync(RunHalfCarDynamicAnalysisRequest request)
        {
            double rearDamping = request.DampingRatio * 2 * Math.Sqrt(request.Mass * request.RearStiffness);
            double frontDamping = request.DampingRatio * 2 * Math.Sqrt(request.Mass * request.FrontStiffness);

            var dynamicModel = new DynamicModel();

            var tasks = new List<Task>();

            double[,] mass = null;
            tasks.Add(Task.Run(() =>
                mass = this.BuildMassMatrix(request.Mass, request.MomentOfInertia, request.DriverMass, request.EngineMass)));

            double[,] stiffness = null;
            tasks.Add(Task.Run(() =>
                stiffness = this.BuildStiffnessMatrix(request.RearStiffness, request.RearDistance, request.FrontStiffness,
                    request.FrontDistante, request.DriverStiffness, request.DriverDistance, request.EngineStiffness, request.EngineDistance)));

            double[,] damping = null;
            tasks.Add(Task.Run(() =>
                damping = this.BuildDampingMatrix(rearDamping, request.RearDistance, frontDamping, request.FrontDistante)));

            double[] force = null;
            tasks.Add(Task.Run(() =>
                dynamicModel.Force = this.BuildForceVector(request.Mass, request.DriverMass, request.EngineMass)));

            double[,] basicExcitationDamping = null;
            tasks.Add(Task.Run(() => basicExcitationDamping =
                this.BuildBasicExcitationDampingMatrix(rearDamping, request.RearDistance, frontDamping, request.FrontDistante)));

            double[,] basicExcitationStifness = null;
            tasks.Add(Task.Run(() => basicExcitationStifness = this.BuildBasicExcitationStiffnessMatrix(
                request.RearStiffness, request.RearDistance, request.FrontStiffness, request.FrontDistante)));

            await Task.WhenAll(tasks);

            return new DynamicModel
            {
                Mass = mass,
                Stiffness = stiffness,
                Damping = damping,
                Force = force,
                BasicExcitationDamping = basicExcitationDamping,
                BasicExcitationStifness = basicExcitationStifness,
                BasicExcitationVelocitity = new double[this._numberOfBoundaryConditions],
                BasicExcitationDisplacement = new double[this._numberOfBoundaryConditions]
            };
        }

        public NumericalMethodInput BuildNumericalMethodInput(DynamicModel dynamicModel, uint numberOfBoundaryConditions, double timeStep)
        {
            return new NumericalMethodInput
            {
                Mass = dynamicModel.Mass,
                Damping = dynamicModel.Damping,
                Stiffness = dynamicModel.Stiffness,
                EquivalentForce = dynamicModel.Force.Sum(
                    dynamicModel.BasicExcitationDamping.Multiply(dynamicModel.BasicExcitationVelocitity),
                    dynamicModel.BasicExcitationStifness.Multiply(dynamicModel.BasicExcitationDisplacement)),
                NumberOfBoundaryConditions = numberOfBoundaryConditions,
                TimeStep = timeStep
            };
        }

        protected override async Task<RunHalfCarDynamicAnalysisResponse> ProcessOperationAsync(RunHalfCarDynamicAnalysisRequest request)
        {
            var response = new RunHalfCarDynamicAnalysisResponse();

            DynamicModel dynamicModel = await this.BuildDynamicModelAsync(request).ConfigureAwait(false);

            var previousResult = new NumericalMethodResult();

            double time = 0;

            using (var streamWriter = new StreamWriter(this.CreateSolutionFile()))
            {
                while (time <= request.FinalTime)
                {
                    var basicExcitationVectors = this.BuildBasicExcitationDisplacementAndVelocityVectors(request.Velocity, request.RearDistance + request.FrontDistante, request.ObstacleLength, request.ObstacleHeight, request.ObstacleDistance, time);
                    dynamicModel.BasicExcitationDisplacement = basicExcitationVectors.Displacement;
                    dynamicModel.BasicExcitationVelocitity = basicExcitationVectors.Velocity;

                    NumericalMethodInput numericalMethodInput = this.BuildNumericalMethodInput(dynamicModel, this._numberOfBoundaryConditions, request.TimeStep);

                    NumericalMethodResult result = await this._numericalMethod.CalculateResult(numericalMethodInput, previousResult, time).ConfigureAwait(false);

                    streamWriter.WriteLine(result);

                    time += request.TimeStep;

                    previousResult = result;
                }
            }

            return response;
        }
    }
}
