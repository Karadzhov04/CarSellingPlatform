using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
    public class CarSellingPlatformDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars{ get; set; }
        public DbSet<Ad> Ads{ get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<CategoryAd> CategoriesAds{ get; set; }

        public CarSellingPlatformDbContext() 
        {
            this.Users = this.Set<User>();
            this.Cars = this.Set<Car>();
            this.Ads = this.Set<Ad>();
            this.Categories = this.Set<Category>();
            this.CategoriesAds = this.Set<CategoryAd>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region User Config
            modelBuilder.Entity<User>()
                           .HasKey(x => x.Id);

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Username = "ivankar",
                    Password = "378873",
                    FName = "Ivan",
                    LName = "Karadzhov",
                    Email = "ivan@abv.bg",
                    Phone = "0885862502"
                });
            #endregion

            #region Car Config
            modelBuilder.Entity<Car>()
                            .HasKey(x => x.Id);

            modelBuilder.Entity<Car>()
                            .HasOne(x => x.Owner)
                            .WithMany()
                            .HasForeignKey(x => x.OwnerId)
                            .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Ad Config
            modelBuilder.Entity<Ad>()
                            .HasKey(x => x.Id);

            modelBuilder.Entity<Ad>()
                            .HasOne(x => x.Owner)
                            .WithMany()
                            .HasForeignKey(x => x.OwnerId)
                            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ad>()
                            .HasOne(x => x.Car)
                            .WithMany()
                            .HasForeignKey(x => x.CarId)
                            .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Category Config
            modelBuilder.Entity<Category>()
                           .HasKey(x => x.Id);
            #endregion

            #region CategoryAd Config
            modelBuilder.Entity<CategoryAd>()
                                        .HasKey(x => x.Id);

            modelBuilder.Entity<CategoryAd>()
                            .HasOne(x => x.Ad)
                            .WithMany()
                            .HasForeignKey(x => x.AdId)
                            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoryAd>()
                            .HasOne(x => x.Category)
                            .WithMany()
                            .HasForeignKey(x => x.CategoryId)
                            .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
             .UseSqlServer(@"Server=DESKTOP-RU0BL0N\SQLEXPRESS;Database=CarSellingPlatformDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
