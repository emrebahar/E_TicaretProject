using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_TicaretProject.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Session;
using E_TicaretProject.Interface;
using E_TicaretProject.Repository;

namespace E_TicaretProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            //sayfayi yenileyince yenilikleri algilamasi icin
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddDbContext<E_TicaretDbContext>(options =>
       options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDistributedMemoryCache();
       
            services.AddSession();

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    // Set a short timeout for easy testing.
            //    options.IdleTimeout = TimeSpan.FromSeconds(10);
            //    options.Cookie.HttpOnly = true;
            //});


     
            services.AddScoped<ICartRepository, CartRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            E_TicaretDbSeed.Initialize(app.ApplicationServices, true);

        }
    }
}
