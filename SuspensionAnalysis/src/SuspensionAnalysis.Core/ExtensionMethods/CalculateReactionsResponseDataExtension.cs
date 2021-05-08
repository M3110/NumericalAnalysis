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
                responseData.LowerWishboneReaction1.X + responseData.LowerWishboneReaction2.X
                + responseData.UpperWishboneReaction1.X + responseData.UpperWishboneReaction2.X
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
                responseData.LowerWishboneReaction1.Y + responseData.LowerWishboneReaction2.Y
                + responseData.UpperWishboneReaction1.Y + responseData.UpperWishboneReaction2.Y
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
                responseData.LowerWishboneReaction1.Z + responseData.LowerWishboneReaction2.Z
                + responseData.UpperWishboneReaction1.Z + responseData.UpperWishboneReaction2.Z
                + responseData.ShockAbsorberReaction.Z - appliedForce;
        }
    }
}
