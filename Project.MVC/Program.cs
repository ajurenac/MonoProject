using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Data;
using Project.Service.Data;
using Project.Service.Repositories.Implementations;
using Project.Service.Repositories.Interfaces;
using Project.Service.Services.Implementations;
using Project.Service.Services.Interfaces;

namespace Project.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<VehicleDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => 
                options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<IVehicleMakeRepository, VehicleMakeRepository>();
            builder.Services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
