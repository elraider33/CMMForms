using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { 
                webBuilder.UseStartup<Startup>()
                .UseKestrel(config => config.Limits.MaxRequestBodySize = long.MaxValue);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Information);
            })
            .UseSerilog((HostBuilderContext context, LoggerConfiguration configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
    }
}