using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "BatteryPercentage is required yo")]
    public int BatteryPercentage { get; set; }
    
    [Required(ErrorMessage = "Status is required yo")]
    public bool Status { get; set; }
    
    [Required(ErrorMessage = "ZoneId is required yo")]
    public int ZoneId { get; set; }
}

public class DevicePut
{
    [Required(ErrorMessage = "Id is required yo")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "BatteryPercentage is required yo")]
    public int BatteryPercentage { get; set; }
    
    [Required(ErrorMessage = "Status is required yo")]
    public bool Status { get; set; }
    
    [Required(ErrorMessage = "ZoneId is required yo")]
    public int ZoneId { get; set; }
}
