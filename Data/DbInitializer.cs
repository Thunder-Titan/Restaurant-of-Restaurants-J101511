using RestaurantofRestaurants.Models;

namespace RestaurantofRestaurants.Data
{
    public class DbInitializer
    {
        public static void Initialize(RestaurantofRestaurantsContext context)
        {
            if(context.FoodItems.Any())
            {
                return;
            }

            var fooditems = new FoodItem[]
            {
                new FoodItem{Item_name="Cheesy Cheeseburger",Item_desc="An extra cheesy cheeseburger with entirely too much cheese",Available=true,Vegetarian=false},
                new FoodItem{Item_name="Clouds n' Cabbage",Item_desc="A salad featuring many mashed potatoes",Available = true,Vegetarian=true},
                new FoodItem{Item_name="Dragon's Breath Beef",Item_desc="A traditional Chilli with a lot of spice",Available=true,Vegetarian=false},
                new FoodItem{Item_name="Clothes Store Special",Item_desc="Our specialty soup, which is not at the clothes store, Jim.",Available=true,Vegetarian=false},
                new FoodItem{Item_name="A Literal Black Hole",Item_desc="Reimagining the term 'Spaghettification' with our excellent Spaghetti bolognese",Available=true,Vegetarian=false}
            };

            context.FoodItems.AddRange(fooditems);
            context.SaveChanges();
        }
    }
}
