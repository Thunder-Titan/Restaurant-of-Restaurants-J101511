using Microsoft.AspNetCore.Identity;

namespace RestaurantofRestaurants.Data
{
    public class IdentitySeedData
    {
        public static async Task Initialize(RestaurantofRestaurantsContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string adminRole = "Admin";
            string memberRole = "Member";
            string password4all = "P@55word";

            if (await roleManager.FindByNameAsync(adminRole) == null) 
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (await roleManager.FindByNameAsync(memberRole) == null) 
            {
                await roleManager.CreateAsync(new IdentityRole(memberRole));
            }

            if (await userManager.FindByNameAsync("admin@bleepblorp.com") == null) 
            {
                var user = new IdentityUser
                {
                    UserName = "admin@bleepblorp.com",
                    Email = "admin@bleepblorp.com",
                };  

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) 
                { 
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }

            if (await userManager.FindByNameAsync("member@bleepblorp.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "member@bleepblorp.com",
                    Email = "member@bleepblorp.com",
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }
        }
    }
}
