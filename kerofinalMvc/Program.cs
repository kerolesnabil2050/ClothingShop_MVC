using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Project.Hubs;
using Project.Models;
using Project.Repoitry;
using SharpDX.DXGI;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(option =>
                {
                    option.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
                }).AddNewtonsoftJson(op =>
                {
                    op.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

         
                
            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Cs"));
            });
            builder.Services.AddIdentity<ApplcationUser, IdentityRole>().AddEntityFrameworkStores<Context>();
            builder.Services.AddScoped<IsupplierRepositry,SupplierRepositry>();
            builder.Services.AddScoped<IProductRepositry, ProductRepositry>();
            builder.Services.AddScoped<IProductSellerRepositry, ProductSellereRepositry>();
            builder.Services.AddScoped<ICategoryRepositry, CategoryRepositry>();
            builder.Services.AddScoped<IcustomerProductRepoitry,CustomerProductRepositry>();
            builder.Services.AddScoped<IShipeerRepositry, ShipperRepositry>();
            builder.Services.AddScoped<IOrderRepositry, OrderRepositry>();
            builder.Services.AddScoped<IOrderProductCustomerRepostry, OrderProductCustomerRepostry>();

            builder.Services.AddScoped<ICustomerRepositry, CustomerRepositry>();



			builder.Services.AddSignalR();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
			app.MapHub<SupplierHub>("/SupplierHub");
			app.MapControllerRoute(
                name: "default",
                //Product/Index
                pattern: "{controller=Product}/{action=Index}/{id?}");

            app.Run();
        }
    }
}