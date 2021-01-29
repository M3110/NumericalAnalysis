using SuspensionAnalysis.Core.Models;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Mapper
{
    public class MappingResolver : IMappingResolver
    {
        /// <summary>
        /// This method creates a <see cref="SuspensionSystem{Profile}"/> based on <see cref="CalculateReactionsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SuspensionSystem<Profile> MapFrom(CalculateReactionsRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new SuspensionSystem<Profile>
            {
                ShockAbsorber = ShockAbsorber.Create(request.ShockAbsorber),
                SuspensionAArmLower = SuspensionAArm<Profile>.Create(request.SuspensionAArmLower),
                SuspensionAArmUpper = SuspensionAArm<Profile>.Create(request.SuspensionAArmUpper),
                TieRod = TieRod<Profile>.Create(request.TieRod)
            };
        }
    }
}
