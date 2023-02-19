using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class Hospital2DbContext : IdentityDbContext<AppUser>
{
    public Hospital2DbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    public DbSet<Home> Home { get; set; }
    public DbSet<About> About { get; set; }
    public DbSet<Button> Buttons { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Icon> Icons { get; set; }
    public DbSet<Rezerv> Rezervs { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Setting> Setting { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Position> Positions { get; set; }
}

