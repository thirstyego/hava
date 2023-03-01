namespace apichipper.Models;

public class Zone
{
    public int Id { get; set; }
    public int HomeId { get; set; }
    public Home Home { get; set; }
    public ICollection<Device> Devices { get; set; }
}

public class ZonePost
{
    public int HomeId { get; set; }
}

public class ZonePut
{
    public int Id { get; set; }
    public int HomeId { get; set; }
}
