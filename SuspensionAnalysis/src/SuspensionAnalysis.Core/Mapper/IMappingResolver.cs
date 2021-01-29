using SuspensionAnalysis.Core.Models;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Mapper
{
    public interface IMappingResolver
    {
        /// <summary>
        /// This method creates a <see cref="SuspensionSystem{Profile}"/> based on <see cref="CalculateReactionsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SuspensionSystem<Profile> MapFrom(CalculateReactionsRequest request);
    }
}