using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantofRestaurants.Data
{
    public class CheckoutCustomer
    {
        [Key]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int BasketID { get; set; }
    }
    public class Basket
    {
        [Key]
        public int BasketID { get; set; }
    }
    public class BasketItem
    {
        [Required]
        public int StockID { get; set; }
        [Required]
        public int BasketID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
