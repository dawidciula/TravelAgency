using Microsoft.EntityFrameworkCore;
using UbbRentalBike.Data;
using UbbRentalBike.Repository;
using UbbRentalBike.Services;
using FluentValidation;
using UbbRentalBike.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    options => options.SignIn.RequireConfirmedAccount = true
)
.AddRoles<IdentityRole>()
.AddDefaultTokenProviders()
.AddDefaultUI()
.AddEntityFrameworkStores<UbbRentalBikeContext>();
builder.Services.AddRazorPages();

//Dodanie polityk autoryzacji
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
    options.AddPolicy("Member", policy => policy.RequireRole("Member"));
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();