using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantofRestaurants.Data;
using RestaurantofRestaurants.Models;
using Microsoft.AspNetCore.Identity;

namespace RestaurantofRestaurants.Pages.Menu
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RestaurantofRestaurantsContext _db;
        private readonly RestaurantofRestaurants.Data.RestaurantofRestaurantsContext _context;
        public IndexModel(RestaurantofRestaurantsContext db, UserManager<IdentityUser> userManager, RestaurantofRestaurants.Data.RestaurantofRestaurantsContext context)
        {
            _db = db;
            _userManager = userManager;
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

        public async Task<IActionResult> OnPostBuyAsync(int itemID)
        {
            var user = await _userManager.GetUserAsync(User);
            CheckoutCustomer customer = await _db
            .CheckoutCustomers
            .FindAsync(user.Email);

            var item = _db.BasketItem
                .FromSqlRaw("SELECT * FROM BasketItems WHERE StockID = {0}" + 
                "AND BasketID = {1}", itemID, customer.BasketID)
                       .ToList()
                       .FirstOrDefault();

            if (item == null) 
            {
                BasketItem newItem = new BasketItem
                {
                    BasketID = customer.BasketID,
                    StockID = itemID,
                    Quantity = 1
                };
                _db.BasketItem.Add(newItem);
                await _db.SaveChangesAsync();
            }
            else
            {
                item.Quantity = item.Quantity + 1;
                _db.Attach(item).State = EntityState.Modified;
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e) 
                {
                    throw new Exception($"Basket not found!", e);
                }
            }
            return RedirectToPage();
        }
    }
}
