using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantofRestaurants.Data;
using RestaurantofRestaurants.Models;

namespace RestaurantofRestaurants.Pages.Menu
{
    public class EditModel : PageModel
    {
        private readonly RestaurantofRestaurants.Data.RestaurantofRestaurantsContext _context;

        public EditModel(RestaurantofRestaurants.Data.RestaurantofRestaurantsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FoodItem FoodItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FoodItems == null)
            {
                return NotFound();
            }

            var fooditem =  await _context.FoodItems.FirstOrDefaultAsync(m => m.ID == id);
            if (fooditem == null)
            {
                return NotFound();
            }
            FoodItem = fooditem;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FoodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(FoodItem.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FoodItemExists(int id)
        {
          return _context.FoodItems.Any(e => e.ID == id);
        }
    }
}
