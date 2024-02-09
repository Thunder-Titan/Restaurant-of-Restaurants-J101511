using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantofRestaurants.Data;

namespace RestaurantofRestaurants.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly RestaurantofRestaurantsContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public IList<CheckoutItems> Items { get; private set; }

        public decimal Total;
        public long AmountPayable;

        public CheckoutModel(RestaurantofRestaurantsContext db, UserManager<IdentityUser> UserManager)
        {
            _db = db;
            _userManager = UserManager;
        }
        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            CheckoutCustomer customer = await _db.CheckoutCustomers.FindAsync(user.Email);

            Items = _db.CheckoutItems.FromSqlRaw(
                "SELECT FoodItem.ID, FoodItem.Price, " +
                "FoodItem.Item_name, " +
                "BasketItems.BasketID, BasketItems.Quantity " +
                "FROM FoodItem INNER JOIN BasketItems " +
                "ON FoodItem.ID = BasketItems.StockID " +
                "WHERE BasketID = {0}", customer.BasketID
                ).ToList();

            Total = 0;

            foreach ( var item in Items )
            {
                Total += (item.Quantity * item.Price);
            }
            AmountPayable = (long)Total;
        }
    }
}
