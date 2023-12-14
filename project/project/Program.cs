using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using project.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<projectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("projectContext") ?? throw new InvalidOperationException("Connection string 'projectContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(1); });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Useralls}/{action=login}/{id?}");

app.Run();
