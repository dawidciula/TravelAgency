using System.Globalization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Data;
using UbbRentalBike.Repository;
using UbbRentalBike.Services;
using FluentValidation;
using UbbRentalBike.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

//Konfiguracja Localization
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-GB"),
        new CultureInfo("pl-PL")
    };

    options.DefaultRequestCulture = new RequestCulture("pl-PL");
    options.SupportedUICultures = supportedCultures;
});

// Dodaj DbContext do kontenera wstrzykiwania zależności
builder.Services.AddDbContext<RentalContext>((serviceProvider, options) =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 3, 0)));
});
builder.Services.AddDbContext<UbbRentalBikeContext>((serviceProvider, options) =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("UbbRentalBikeContextConnection"), 
        new MySqlServerVersion(new Version(8, 3, 0)));
});

// Dodaj repozytorium do kontenera wstrzykiwania zależności
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();

//Zarejestrowanie walidacji
builder.Services.AddValidatorsFromAssemblyContaining<ParticipantValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ReservationValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TripValidation>();

//Zarejestrowanie automapper'a
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Konfiguracja Identity
builder.Services.AddDefaultIdentity<IdentityUser>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        //Dodanie 'claim' dla uzytkownikow
        options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
        options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
        options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
    }
)
.AddRoles<IdentityRole>()
.AddDefaultTokenProviders()
.AddDefaultUI()
.AddEntityFrameworkStores<UbbRentalBikeContext>();
builder.Services.AddRazorPages();

//Dodanie polityk autoryzacji
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerOrAdmin", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Manager", "Admin");
    });
});



var app = builder.Build();

// Create database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<RentalContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        // Handle any errors while creating the database
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

//Skonfigurowanie ról do bazy danych
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Manager", "Member" };
 
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string email = "admin@admin.com";
    string password = "Admin123@";
    
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;

        await userManager.CreateAsync(user, password);

        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string email = "manager@manager.com";
    string password = "Manager123@";
    
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;

        await userManager.CreateAsync(user, password);

        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Manager"));
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();