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
    public string Name { get; set; }
    public string ApplicationUserId { get; set; }
}

public class HomePut
{
    public int Id { get; set; }
    public string Name { get; set; }
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