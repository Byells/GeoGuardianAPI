namespace GeoGuardian.Entities;

public class SensorModel
{
    public int SensorModelId { get; set; }
    public string Name       { get; set; } = null!;
    public string? Maker     { get; set; }

    public ICollection<Sensor> Sensors { get; set; } = [];
}
