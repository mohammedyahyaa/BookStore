using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
             CreateHostBuilder(args).Build().Run();

            //var webHost = CreateHostBuilder(args).Build();
            //RunMigrations(webHost);
            //webHost.Run();

        }


        // Perform Migrations on DB
        //private static void RunMigrations(IWebHost webHost)
        //{
        //    using (var scope = webHost.Services.CreateScope())
        //    {
        //        var db = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
        //        db.Database.Migrate();
        //    }
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
