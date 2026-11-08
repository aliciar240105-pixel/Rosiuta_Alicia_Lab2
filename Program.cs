using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rosiuta_Alicia_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Rosiuta_Alicia_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Rosiuta_Alicia_Lab2Context' not found.");

builder.Services.AddDbContext<Rosiuta_Alicia_Lab2Context>(options => options.UseSqlServer(connectionString));

// contextul pentru autentificare (Identity) - foloseste aceeasi baza de date
builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryIdentityContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
