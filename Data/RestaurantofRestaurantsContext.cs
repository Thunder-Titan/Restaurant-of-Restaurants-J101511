using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantofRestaurants.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RestaurantofRestaurants.Data
{
    public class RestaurantofRestaurantsContext : IdentityDbContext
    {
        public RestaurantofRestaurantsContext (DbContextOptions<RestaurantofRestaurantsContext> options)
            : base(options)
        {
        }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<CheckoutCustomer> CheckoutCustomers { get; set; } = default!;
        public DbSet<Basket> Baskets { get; set; } = default!;
        public DbSet<BasketItem> BasketItem { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FoodItem>().ToTable("FoodItem");

            modelBuilder.Entity<BasketItem>().HasKey(t => new { t.StockID, t.BasketID });
        }
    }
}
