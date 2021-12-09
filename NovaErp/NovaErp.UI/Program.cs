using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            try
            {
                Log.Logger = new LoggerConfiguration()
               .WriteTo.Seq("http://localhost:5341")
               //.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
               .MinimumLevel.Information()
               .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
               .Enrich.WithProperty("AppName", "Serilog Sample")
               .Enrich.WithProperty("Environment", "development")
               //.Enrich.With(new ThreadIdEnricher())

               .CreateLogger();
                Log.Information("NOVA ERP WEB DEMO LOG ÝÞLEMLERÝ");
                CreateHostBuilder(args).Build().Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseUrls("http://localhost:5001", "https://localhost:5004");
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(config => config.ClearProviders()).UseSerilog();
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args)
        //    .ConfigureWebHostDefaults(webBuilder =>
        //    {
        //        webBuilder.UseStartup<Startup>();
        //        webBuilder.UseUrls("http://localhost:6000", "https://localhost:5004");
        //    });
    }
}

