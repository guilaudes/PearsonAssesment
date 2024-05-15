using Microsoft.EntityFrameworkCore;
using PearsonAssesment.Models;
using System.Collections.Generic;
namespace EFCoreInMemoryDbDemo
{
    public class PearsonAssesmentContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "PearsonAssesment");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(e => e.ParentUser).WithMany(e => e.FollowersList).HasForeignKey(e => new { e.ParentUserId });
            modelBuilder.Entity<User>().HasMany(e => e.PostList).WithOne(e => e.User).HasForeignKey(e => new { e.Username});

        }
    }

    
}