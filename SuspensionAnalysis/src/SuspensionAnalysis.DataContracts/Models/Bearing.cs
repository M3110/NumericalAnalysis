using SuspensionAnalysis.DataContracts.Models.Enums;
using System;

namespace SuspensionAnalysis.DataContracts.Models
{
    /// <summary>
    /// It contains the necessary information about each material that could be used in project.
    /// </summary>
    public struct Bearing
    {
        /// <summary>
        /// It contains the necessary information about Steel SAE 1020.
        /// </summary>
        public static readonly Bearing Steel1020 = new Bearing(youngModulus: 205e9, yieldStrength: 350e6, specificMass: 7850);

        /// <summary>
        /// It contains the necessary information about Steel SAE 4130.
        /// </summary>
        public static readonly Bearing Steel1045 = new Bearing(youngModulus: 200e9, yieldStrength: 450e6, specificMass: 7850);

        /// <summary>
        /// It contains the necessary information about Aluminum 6061-T6.
        /// </summary>
        public static readonly Bearing Aluminum6061T6 = new Bearing(youngModulus: 70e9, yieldStrength: 310e6, specificMass: 2710);

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="youngModulus"></param>
        /// <param name="specificMass"></param>
        /// <param name="yieldStrength"></param>
        private Bearing(double youngModulus, double yieldStrength, double specificMass)
        {
            this.YoungModulus = youngModulus;
            this.YieldStrength = yieldStrength;
            this.SpecificMass = specificMass;
        }

        /// <summary>
        /// Young modulus. 
        /// Unit: Pa (Pascal).
        /// </summary>
        public double YoungModulus { get; }

        /// <summary>
        /// Yield strength. 
        /// Unit: Pa (Pascal).
        /// </summary>
        public double YieldStrength { get; }

        /// <summary>
        /// Specific mass. 
        /// Unit: kg/m³ (kilogram per cubic meters).
        /// </summary>
        public double SpecificMass { get; }

        /// <summary>
        /// This method creates an instance of class <seealso cref="Bearing"/>.
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        public static Bearing Create(MaterialType materialType)
        {
            return materialType switch
            {
                MaterialType.Steel1020 => Bearing.Steel1020,
                MaterialType.Steel1045 => Bearing.Steel1045,
                MaterialType.Aluminum6061T6 => Bearing.Aluminum6061T6,

                _ => throw new Exception($"Invalid material: '{materialType}'.")
            };
        }
    }
}
