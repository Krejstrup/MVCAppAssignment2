using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MVCAppAssignment2.Database;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Repo;
using System;

namespace MVCAppAssignment2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost hB = CreateHostBuilder(args).Build(); // Step 3
                                                        // Do more stuff with the Build here:
                                                        // Seed the user roles into the program. Run the CreateDbIfNotExist
            CreateDbIfNotExist(hB);

            hB.Run();   // Start up the server!
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static void CreateDbIfNotExist(IHost host)  // Step 2: host is the program running witin the server
        {
            using (var scope = host.Services.CreateScope()) // A bit of injection. Create the scope for below.
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<PeopleDbContext>();   // Access the database. Step 4:
                    RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    DbInitializer.Initialize(context, roleManager, userManager); // Send over connections to database and the managers

                }
                catch (Exception ex) // If somthing crashes
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while creating the Db.");
                }
            }
        }
    }
}
