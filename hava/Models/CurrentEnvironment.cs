namespace hava.Models;

public class CurrentEnvironment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Temperature { get; set; }
    public string TargetTemperature { get; set; }
    public string Humidity { get; set; }
    public string TargetHumidity { get; set; }
    public Mode Mode { get; set; }
    public string Date { get; set; }
    public int ZoneId { get; set; }
    public Zone Zone { get; set; }
}

public class CurrentEnvironmentPost
{
    public string Name { get; set; }
    public string Temperature { get; set; }
    public string TargetTemperature { get; set; }
    public string Humidity { get; set; }
    public string TargetHumidity { get; set; }
    public Mode Mode { get; set; }
    public string Date { get; set; }
    public int ZoneId { get; set; }
}

public class CurrentEnvironmentPut
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Temperature { get; set; }
    public string TargetTemperature { get; set; }
    public string Humidity { get; set; }
    public string TargetHumidity { get; set; }
    public Mode Mode { get; set; }
    public string Date { get; set; }
    public int ZoneId { get; set; }
}

public enum Mode
{
    Heat,
    Cool,
    Both,
    Off
}
