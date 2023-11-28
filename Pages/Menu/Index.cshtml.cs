using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantofRestaurants.Data;
using RestaurantofRestaurants.Models;

namespace RestaurantofRestaurants.Pages.Menu
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantofRestaurants.Data.RestaurantofRestaurantsContext _context;

        public IndexModel(RestaurantofRestaurants.Data.RestaurantofRestaurantsContext context)
        {
            _context = context;
        }

        public IList<FoodItem> FoodItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FoodItems != null)
            {
                FoodItem = await _context.FoodItems.ToListAsync();
            }
        }
    }
}
