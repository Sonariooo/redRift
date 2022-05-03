using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedRift.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRift.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Player> Players { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<RedRift.Models.Game> Games { get; set; }
        public DbSet<RedRift.Models.GameResult> GameResults { get; set; }
    }
}
