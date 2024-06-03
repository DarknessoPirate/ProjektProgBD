using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgBD.Models
{
    public class ShopDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }
       
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserGame> UserGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(eb =>
            {
                eb.HasMany(x => x.Games)
                .WithMany(x => x.Users)
                .UsingEntity<UserGame>(

                    w => w.HasOne(x => x.Game)
                    .WithMany()
                    .HasForeignKey(x => x.GameId),

                    w => w.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId),

                    w => w.Property(x => x.TransactionDate).HasDefaultValueSql("getutcdate()")

                    );
            });

            modelBuilder.Entity<Game>(eb =>
            {
                
            });

            modelBuilder.Entity<Review>(eb =>
            {
                eb.HasOne(x => x.User)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.UserId);

                eb.HasOne(x => x.Game)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.GameId);    
            });
        }
    }
}
