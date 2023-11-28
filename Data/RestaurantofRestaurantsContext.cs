using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantofRestaurants.Models;

namespace RestaurantofRestaurants.Data
{
    public class RestaurantofRestaurantsContext : DbContext
    {
        public RestaurantofRestaurantsContext (DbContextOptions<RestaurantofRestaurantsContext> options)
            : base(options)
        {
        }

        public DbSet<FoodItem> FoodItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodItem>().ToTable("FoodItem");
        }
    }
}
