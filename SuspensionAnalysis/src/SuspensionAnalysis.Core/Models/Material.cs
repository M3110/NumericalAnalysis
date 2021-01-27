using SuspensionAnalysis.Infraestructure.Models.Enums;
using System;

namespace SuspensionAnalysis.Infraestructure.Models
{
    /// <summary>
    /// It contains the necessary information about each material that could be used in project.
    /// </summary>
    public struct Material
    {
        /// <summary>
        /// It contains the necessary information about Steel SAE 1020.
        /// YieldStrength => 350e6;
        /// </summary>
        public static readonly Material Steel1020 = new Material(youngModulus: 205e9, yieldStrength: , specificMass: 7850);

        /// <summary>
        /// It contains the necessary information about Steel SAE 4130.
        /// YieldStrength => 460e6
        /// </summary>
        public static readonly Material Steel4130 = new Material(youngModulus: 200e9, yieldStrength:, specificMass: 7850);

        /// <summary>
        /// It contains the necessary information about Aluminum.
        /// YieldStrength => 300e6
        /// </summary>
        public static readonly Material Aluminum6061T6 = new Material(youngModulus: 70e9, yieldStrength: 310e6, specificMass: 2710);

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="youngModulus"></param>
        /// <param name="specificMass"></param>
        private Material(double youngModulus, double yieldStrength, double specificMass)
        {
            this.YoungModulus = youngModulus;
            this.YieldStrength = yieldStrength;
            this.SpecificMass = specificMass;
        }

        /// <summary>
        /// Young modulus. Unity: Pa (Pascal).
        /// </summary>
        public double YoungModulus { get; }

        /// <summary>
        /// Yield strength. Unity: Pa (Pascal).
        /// </summary>
        public double YieldStrength { get; }

        /// <summary>
        /// Specific mass. Unity: kg/m³ (kilogram per cubic meters).
        /// </summary>
        public double SpecificMass { get; }

        /// <summary>
        /// This method creates an instance of class <seealso cref="Material"/>.
        /// It can be <seealso cref="Steel1020"/>, <seealso cref="Steel4130"/> or <seealso cref="Aluminum"/>.
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public static Material Create(MaterialType materialType) => materialType switch
        {
            MaterialType.Steel1020 => Steel1020,

            _ => throw new Exception($"Invalid material: '{materialType}'.")
        };
    }
}
