using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Name is required yo")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Temperature is required yo")]
    public string Temperature { get; set; }
    
    [Required(ErrorMessage = "TargetTemperature is required yo")]
    public string TargetTemperature { get; set; }
    
    [Required(ErrorMessage = "Humidity is required yo")]
    public string Humidity { get; set; }
    
    [Required(ErrorMessage = "TargetHumidity is required yo")]
    public string TargetHumidity { get; set; }
    
    [Required(ErrorMessage = "Mode is required yo")]
    public Mode Mode { get; set; }
    
    [Required(ErrorMessage = "Date is required yo")]
    public string Date { get; set; }
    
    [Required(ErrorMessage = "ZoneId is required yo")]
    public int ZoneId { get; set; }
}

public class CurrentEnvironmentPut
{
    [Required(ErrorMessage = "Id is required yo")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required yo")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Temperature is required yo")]
    public string Temperature { get; set; }
    
    [Required(ErrorMessage = "TargetTemperature is required yo")]
    public string TargetTemperature { get; set; }
    
    [Required(ErrorMessage = "Humidity is required yo")]
    public string Humidity { get; set; }
    
    [Required(ErrorMessage = "TargetHumidity is required yo")]
    public string TargetHumidity { get; set; }
    
    [Required(ErrorMessage = "Mode is required yo")]
    public Mode Mode { get; set; }
    
    [Required(ErrorMessage = "Date is required yo")]
    public string Date { get; set; }
    
    [Required(ErrorMessage = "ZoneId is required yo")]
    public int ZoneId { get; set; }
}

public enum Mode
{
    Heat,
    Cool,
    Both,
    Off
}
