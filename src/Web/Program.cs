using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseNServiceBus(context =>
                {
                    var endpointConfiguration = new EndpointConfiguration("guesswork.web");
                    var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
                    transport.ConnectionString(context.Configuration.GetValue<string>("ASB_CON_STRING"));
                    transport.SubscriptionNameShortener(x => x.Split('.').Last());
                    endpointConfiguration.EnableInstallers();
                    
                    // transport.Routing().RouteToEndpoint(
                    //     assembly: typeof(MyMessage).Assembly,
                    //     destination: "Samples.ASPNETCore.Endpoint");


                    return endpointConfiguration;
                });
    }

}