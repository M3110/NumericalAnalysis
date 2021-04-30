﻿using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.OperationBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuspensionAnalysis.DataContracts.CalculateSteeringKnuckleReactions
{
    /// <summary>
    /// It represents the request content to  CalculateSteeringKnuckleReactions operation.
    /// </summary>
    public class CalculateSteeringKnuckleReactionsRequest : OperationRequestBase
    {
        /// <summary>
        /// The steering knuckle points. 
        /// </summary>
        public SteeringKnucklePoint SteeringKnuckle { get; set; }
    }
}