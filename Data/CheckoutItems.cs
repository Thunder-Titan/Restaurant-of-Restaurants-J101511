using System.ComponentModel.DataAnnotations;

namespace RestaurantofRestaurants.Data
{
    public class CheckoutItems
    {
        [Key, Required]
        public int ID { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, StringLength(50)]
        public string Item_Name { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
