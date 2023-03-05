using hava.Models;
using hava.Data;


namespace hava;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        
        if (context.Users.Any())
        {
            return;
        }

        var user1 = new ApplicationUser() { Email = "obama@gmail.com", UserName = "obama", PasswordHash = "ffff" };
        context.Users.Add(user1);
        
        var user2 = new ApplicationUser() { Email = "grandma@gmail.com", UserName = "grandma", PasswordHash = "ffff" };
        context.Users.Add(user2);
        
        var user3 = new ApplicationUser() { Email = "chicken@gmail.com", UserName = "chicken", PasswordHash = "ffff" };
        context.Users.Add(user3);
        
        context.SaveChanges();
        

        
        var home1 = new Home { Name = "Fox", Date = DateTime.Now.ToString(), ApplicationUserId = user1.Id };
        context.Homes.Add(home1);
        
        var home2 = new Home { Name = "Lafayette", Date = DateTime.Now.ToString(), ApplicationUserId = user2.Id };
        context.Homes.Add(home2);
        
        var home3 = new Home { Name = "Roseville", Date = DateTime.Now.ToString(), ApplicationUserId = user3.Id };
        context.Homes.Add(home3);
        
        context.SaveChanges();



        var zone1 = new Zone { Name = "kitchen", HomeId = home1.Id };
        context.Zones.Add(zone1);

        var zone2 = new Zone { Name = "master bedroom", HomeId = home2.Id };
        context.Zones.Add(zone2);

        var zone3 = new Zone { Name = "living room", HomeId = home3.Id };
        context.Zones.Add(zone3);

        context.SaveChanges();



        var device1 = new Device { BatteryPercentage = 45, Status = true, ZoneId = zone1.Id };
        context.Devices.Add(device1);

        var device2 = new Device { BatteryPercentage = 45, Status = true, ZoneId = zone2.Id };
        context.Devices.Add(device2);

        var device3 = new Device { BatteryPercentage = 45, Status = true, ZoneId = zone3.Id };
        context.Devices.Add(device3);

        context.SaveChanges();






    }
}