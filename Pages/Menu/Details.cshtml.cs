﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly RestaurantofRestaurants.Data.RestaurantofRestaurantsContext _context;

        public DetailsModel(RestaurantofRestaurants.Data.RestaurantofRestaurantsContext context)
        {
            _context = context;
        }

      public FoodItem FoodItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FoodItems == null)
            {
                return NotFound();
            }

            var fooditem = await _context.FoodItems.FirstOrDefaultAsync(m => m.ID == id);
            if (fooditem == null)
            {
                return NotFound();
            }
            else 
            {
                FoodItem = fooditem;
            }
            return Page();
        }
    }
}
