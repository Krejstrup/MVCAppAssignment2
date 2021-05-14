using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCAppAssignment2.Models.Repo;
using MVCAppAssignment2.Models.Service;

namespace MVCAppAssignment2
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        //== This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //--- Add the connection DbContext to ConfigServices (as #5) ----------------------
            services.AddDbContext<PeopleDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //==== Register the Dependency Injections to the service collection (as #6) ===:
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ILanguageService, LanguageService>();


            services.AddScoped<IPeopleRepo, DatabasePeopleRepo>();
            services.AddScoped<ICityRepo, DatabaseCityRepo>();
            services.AddScoped<ICountryRepo, DatabaseCountryRepo>();
            services.AddScoped<ILanguageRepo, DatabaseLanguageRepo>();
            services.AddScoped<IPersonLanguage, DatabasePersonLanguageRepo>();



            services.AddControllersWithViews().AddRazorRuntimeCompilation();    // Original setup

        }   // remove the migration with: ef migrations remove
            // When any changes of the data models for the database -> make new migration!
            // dotnet ef migrations add
            // dotnet ef database update



        //== This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "PeopleHandling",
                    pattern: "{controller=People}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "AjaxHandling",
                    pattern: "{controller=AJAX}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Countries",
                    pattern: "{controller=Country}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Cities",
                    pattern: "{controller=Cities}/{action=Index}/{id?}");
            });
        }
    }
}
