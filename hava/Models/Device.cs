namespace hava.Models;

public class Device
{
    public int Id { get; set; }
    public int BatteryPercentage { get; set; }
    public bool Status { get; set; }
    public int ZoneId { get; set; }
    public Zone Zone { get; set; }
}

public class DevicePost
{
    public int BatteryPercentage { get; set; }
    public bool Status { get; set; }
    public int ZoneId { get; set; }
}

public class DevicePut
{
    public int Id { get; set; }
    public int BatteryPercentage { get; set; }
    public bool Status { get; set; }
    public int ZoneId { get; set; }
}
