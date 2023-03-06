using hava.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace hava.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
            
        // public DbSet<User> Users { get; set; }
        // public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CurrentEnvironment> CurrentEnvironments { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<Zone> Zones { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
                
            modelBuilder.Entity<Home>()
                .HasMany<Zone>(h => h.Zones)
                .WithOne(z => z.Home)
                .HasForeignKey(z => z.HomeId);
            
            modelBuilder.Entity<Zone>()
                .HasMany<Device>(h => h.Devices)
                .WithOne(z => z.Zone)
                .HasForeignKey(z => z.ZoneId);
            
            modelBuilder.Entity<ApplicationUser>()
                .HasMany<Home>(h => h.Homes)
                .WithOne(z => z.ApplicationUser)
                .HasForeignKey(z => z.ApplicationUserId);
            
        }
        
        public override int SaveChanges()  
        {  
            ChangeTracker.DetectChanges();  
            return base.SaveChanges();  
        }  
        
    }
}