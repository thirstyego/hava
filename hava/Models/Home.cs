using System.ComponentModel.DataAnnotations;

namespace hava.Models;

public class Home
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Date { get; set; }
    
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public ICollection<Zone> Zones { get; set; }
}

public class HomeGet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Date { get; set; }
    public string ApplicationUserId { get; set; }
}

public class HomePost
{
    [Required(ErrorMessage = "Name is required yo")]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "ApplicationUserId is required yo")]
    public string ApplicationUserId { get; set; }
}

public class HomePut
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required yo")]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "ApplicationUserId is required yo")]
    public string ApplicationUserId { get; set; }
}

public class HomeConverter
{
    public static HomeGet HomeToHomeGet(Home home)
    {
        return new HomeGet()
        {
            Id = home.Id,
            Name = home.Name,
            Date = home.Date,
            ApplicationUserId = home.ApplicationUserId
        };
    }
}