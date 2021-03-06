﻿using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the suspension system.
    /// </summary>
    public class SuspensionSystem
    {
        /// <summary>
        /// The shock absorber.
        /// </summary>
        public ShockAbsorber ShockAbsorber { get; set; }

        /// <summary>
        /// The suspension A-arm upper.
        /// </summary>
        public SuspensionAArm SuspensionAArmUpper { get; set; }

        /// <summary>
        /// The suspension A-arm lower.
        /// </summary>
        public SuspensionAArm SuspensionAArmLower { get; set; }

        /// <summary>
        /// The tie rod.
        /// </summary>
        public TieRod TieRod { get; set; }
    }

    /// <summary>
    /// It represents the suspension system.
    /// </summary>
    public class SuspensionSystem<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// The shock absorber.
        /// </summary>
        public ShockAbsorber ShockAbsorber { get; set; }

        /// <summary>
        /// The suspension A-arm upper.
        /// </summary>
        public SuspensionAArm<TProfile> SuspensionAArmUpper { get; set; }

        /// <summary>
        /// The suspension A-arm lower.
        /// </summary>
        public SuspensionAArm<TProfile> SuspensionAArmLower { get; set; }

        /// <summary>
        /// The tie rod.
        /// </summary>
        public TieRod<TProfile> TieRod { get; set; }
    }
}
