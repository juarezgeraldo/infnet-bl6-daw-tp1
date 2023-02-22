using infnet_bl6_daw_tp1.Domain.Interfaces;
using infnet_bl6_daw_tp1.Service.Services;
using infnet_bl6_daw_tp1.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add service layer
builder.Services.AddScoped<IAmigoService, AmigoService>();

// DB Connection instance
builder.Services.AddDbContext<infnet_bl6_daw_tp1DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AmigoDb")));


var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
