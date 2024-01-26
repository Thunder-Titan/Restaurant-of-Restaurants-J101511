using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantofRestaurants.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RestaurantofRestaurantsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantofRestaurantsContext") ?? throw new InvalidOperationException("Connection string 'RestaurantofRestaurantsContext' not found.")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//   .AddEntityFrameworkStores<RestaurantofRestaurantsContext>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(

options =>
{
    options.Stores.MaxLengthForKeys = 128;
})

.AddEntityFrameworkStores<RestaurantofRestaurantsContext>()
.AddRoles<IdentityRole>()
.AddDefaultUI()
.AddDefaultTokenProviders();


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<RestaurantofRestaurantsContext>();
    context.Database.Migrate();
    var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentitySeedData.Initialize(context, userMgr, roleMgr).Wait();
    //DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
