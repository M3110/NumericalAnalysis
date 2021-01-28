namespace SuspensionAnalysis.Infrastructure.Models.SuspensionComponents
{
    public class ShockAbsorberPoint : SingleComponentPoint 
    { 
        public static ShockAbsorberPoint Create(ShockAbsorber shockAbsorber)
        {
            return new ShockAbsorberPoint
            {
                FasteningPoint = shockAbsorber.FasteningPoint,
                PivotPoint = shockAbsorber.PivotPoint
            };
        }
    }
}
