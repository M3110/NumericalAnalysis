using System.IO;

namespace SuspensionAnalysis.Core.Models
{
    /// <summary>
    /// It contains the base paths used in the application.
    /// </summary>
    public class BasePaths
    {
        /// <summary>
        /// The application base path.
        /// </summary>
        public static string Application => Directory.GetCurrentDirectory().Replace("\\src\\SuspensionAnalysis.Application", "\\");

        /// <summary>
        /// The base path to solution response files.
        /// </summary>
        public static string Solution => Path.Combine(Application, "solutions");

        /// <summary>
        /// The base path to response files of Dynamic Analysis.
        /// </summary>
        public static string DynamicAnalysis => Path.Combine(Solution, "Dynamic Analysis");

        /// <summary>
        /// The base path to response files of Half Car Dynamic Analysis.
        /// </summary>
        public static string HalfCarDynamicAnalysis => Path.Combine(DynamicAnalysis, "Half car");
    }
}
