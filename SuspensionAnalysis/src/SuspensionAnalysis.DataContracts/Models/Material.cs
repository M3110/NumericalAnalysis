using SuspensionAnalysis.DataContracts.Models.Enums;
using System;

namespace SuspensionAnalysis.DataContracts.Models
{
    /// <summary>
    /// It contains the necessary information about each material that could be used in project.
    /// </summary>
    public struct Material
    {
        /// <summary>
        /// It contains the necessary information about Steel SAE 1020.
        /// </summary>
        public static readonly Material Steel1020 = new Material(youngModulus: 205e9, yieldStrength: 350e6, specificMass: 7850);

        /// <summary>
        /// It contains the necessary information about Steel SAE 4130.
        /// </summary>
        public static readonly Material Steel1045 = new Material(youngModulus: 200e9, yieldStrength: 450e6, specificMass: 7850);

        /// <summary>
        /// It contains the necessary information about Aluminum 6061-T6.
        /// </summary>
        public static readonly Material Aluminum6061T6 = new Material(youngModulus: 70e9, yieldStrength: 310e6, specificMass: 2710);

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="youngModulus"></param>
        /// <param name="specificMass"></param>
        /// <param name="yieldStrength"></param>
        private Material(double youngModulus, double yieldStrength, double specificMass)
        {
            this.YoungModulus = youngModulus;
            this.YieldStrength = yieldStrength;
            this.SpecificMass = specificMass;
        }

        /// <summary>
        /// Young modulus. 
        /// Unity: Pa (Pascal).
        /// </summary>
        public double YoungModulus { get; }

        /// <summary>
        /// Yield strength. 
        /// Unity: Pa (Pascal).
        /// </summary>
        public double YieldStrength { get; }

        /// <summary>
        /// Specific mass. 
        /// Unity: kg/m³ (kilogram per cubic meters).
        /// </summary>
        public double SpecificMass { get; }

        /// <summary>
        /// This method creates an instance of class <seealso cref="Material"/>.
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        public static Material Create(MaterialType materialType)
        {
            return materialType switch
            {
                MaterialType.Steel1020 => Material.Steel1020,
                MaterialType.Steel1045 => Material.Steel1045,
                MaterialType.Aluminum6061T6 => Material.Aluminum6061T6,

                _ => throw new Exception($"Invalid material: '{materialType}'.")
            };
        }
    }
}
