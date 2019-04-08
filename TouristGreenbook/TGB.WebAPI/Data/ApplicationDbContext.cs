using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TGB.WebAPI.Models;

namespace TGB.WebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ConcreteUser> ConcreteUsers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Point> Points { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TGB.WebAPI.Models.YourTrips> YourTrips { get; set; }
    }
}
