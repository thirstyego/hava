using System.ComponentModel.DataAnnotations;

namespace hava.Models;

public class Zone
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int HomeId { get; set; }
    public Home Home { get; set; }
    public ICollection<Device> Devices { get; set; }
}

public class ZonePost
{
    [Required(ErrorMessage= "Name is required yo")]
    public string Name { get; set; }
    
    [Required(ErrorMessage= "HomeId is required yo")]
    public int HomeId { get; set; }
}

public class ZonePut
{
    [Required(ErrorMessage = "Id is required yo")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required yo")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "HomeId is required yo")]
    public int HomeId { get; set; }
}
