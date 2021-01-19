using BoostStreamServer.Data.Configurations;
using BoostStreamServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace BoostStreamServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Telemetry> Telemetrys { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
