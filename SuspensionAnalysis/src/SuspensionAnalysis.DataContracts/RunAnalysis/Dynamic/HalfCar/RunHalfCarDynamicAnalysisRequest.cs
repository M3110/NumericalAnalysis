using SuspensionAnalysis.DataContracts.OperationBase;

namespace SuspensionAnalysis.DataContracts.RunAnalysis.Dynamic.HalfCar
{
    public class RunHalfCarDynamicAnalysisRequest : OperationRequestBase
    {
        public double TimeStep { get; set; }

        public double FinalTime { get; set; }

        public double ObstacleLength { get; set; }

        public double ObstacleHeight { get; set; }
        
        public double ObstacleDistance { get; set; }

        public double Velocity { get; set; }

        public double Mass { get; set; }

        public double MomentOfInertia { get; set; } 
        
        public double DriverMass { get; set; }

        public double EngineMass { get; set; }

        public double RearStiffness { get; set; }

        public double FrontStiffness { get; set; }

        public double DriverStiffness { get; set; }

        public double EngineStiffness { get; set; }

        public double DampingRatio { get; set; }

        //public double RearDamping { get; set; }

        //public double FrontDamping { get; set; }

        public double RearDistance { get; set; }

        public double FrontDistante { get; set; }

        public double DriverDistance { get; set; }

        public double EngineDistance { get; set; }
    }
}
