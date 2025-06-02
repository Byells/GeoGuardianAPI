namespace GeoGuardian.Entities;

public class RiskAreaType
{
    public int RiskAreaTypeId { get; set; }          // flood, landslide, dam-break…
    public string Name        { get; set; } = null!;

    public ICollection<RiskArea> RiskAreas { get; set; } = [];
}
