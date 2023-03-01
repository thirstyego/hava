namespace apichipper.Models;

public class Home
{
    public int Id { get; set; }
    public string Date { get; set; }
    
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public ICollection<Zone> Zones { get; set; }
}

public class HomePost
{
    public int UserId { get; set; }
}

public class HomePut
{
    public int Id { get; set; }
    public int UserId { get; set; }
}
