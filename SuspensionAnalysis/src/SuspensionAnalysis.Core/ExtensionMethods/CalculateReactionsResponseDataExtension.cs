using SuspensionAnalysis.DataContracts.CalculateReactions;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    /// <summary>
    /// It contains the extension method to CalculateReactionsResponseData.1
    /// </summary>
    public static class CalculateReactionsResponseDataExtension
    {
        /// <summary>
        /// This method calculates the sum of forces at axis X.
        /// </summary>
        /// <param name="responseData"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static double CalculateForceXSum(this CalculateReactionsResponseData responseData, double appliedForce)
        {
            return
                responseData.AArmLowerReaction1.X + responseData.AArmLowerReaction2.X
                + responseData.AArmUpperReaction1.X + responseData.AArmUpperReaction2.X
                + responseData.ShockAbsorberReaction.X - appliedForce;
        }

        /// <summary>
        /// This method calculates the sum of forces at axis Y.
        /// </summary>
        /// <param name="responseData"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static double CalculateForceYSum(this CalculateReactionsResponseData responseData, double appliedForce)
        {
            return
                responseData.AArmLowerReaction1.Y + responseData.AArmLowerReaction2.Y
                + responseData.AArmUpperReaction1.Y + responseData.AArmUpperReaction2.Y
                + responseData.ShockAbsorberReaction.Y - appliedForce;
        }

        /// <summary>
        /// This method calculates the sum of forces at axis Z.
        /// </summary>
        /// <param name="responseData"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static double CalculateForceZSum(this CalculateReactionsResponseData responseData, double appliedForce)
        {
            return
                responseData.AArmLowerReaction1.Z + responseData.AArmLowerReaction2.Z
                + responseData.AArmUpperReaction1.Z + responseData.AArmUpperReaction2.Z
                + responseData.ShockAbsorberReaction.Z - appliedForce;
        }
    }
}
