namespace GeoGuardian.Entities;

public class SensorReading
{
    public long       SensorReadingId { get; set; }
    public int        SensorId        { get; set; }
    public DateTime   Timestamp       { get; set; } = DateTime.UtcNow;
    public decimal    Value           { get; set; }   

    public Sensor? Sensor { get; set; }
}