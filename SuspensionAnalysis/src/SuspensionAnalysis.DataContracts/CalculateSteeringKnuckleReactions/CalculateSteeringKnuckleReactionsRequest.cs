using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.SuspensionComponents.SteeringKnuckle;
using SuspensionAnalysis.DataContracts.OperationBase;

namespace SuspensionAnalysis.DataContracts.CalculateSteeringKnuckleReactions
{
    /// <summary>
    /// It represents the request content to CalculateSteeringKnuckleReactions operation.
    /// </summary>
    public class CalculateSteeringKnuckleReactionsRequest : OperationRequestBase
    {
        /// <summary>
        /// The request content to CalculateReactions operation. 
        /// </summary>
        public CalculateReactionsRequest CalculateReactionsRequest { get; set; }

        /// <summary>
        /// The 'data' content of operation CalculateReactions response.
        /// </summary>
        public CalculateReactionsResponseData CalculateReactionsResponseData { get; set; }

        /// <summary>
        /// The inertia torque is torque due to brake caliper inertia when coming into contact with the brake disc. 
        /// </summary>
        public string InertiaTorque { get; set; }

        /// <summary>
        /// The engine's torque. 
        /// </summary>
        public string EngineTorque { get; set; }

        /// <summary>
        /// It represents the force that the driver uses when moving the steering wheel. 
        /// </summary>
        public string SteeringWheelForce { get; set; }

        /// <summary>
        /// The steering knuckle position.
        /// It can be <see cref="SuspensionPosition.Rear"/> or <see cref="SuspensionPosition.Front"/>. 
        /// </summary>
        public SuspensionPosition SuspensionPosition { get; set; }

        /// <summary>
        /// The steering knuckle points. 
        /// </summary>
        public SteeringKnucklePoint SteeringKnuckle { get; set; }

        public BearingType Bearing { get; set; }
    }
}
