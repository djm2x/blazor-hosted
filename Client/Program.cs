using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MatBlazor;
using MyBlazor.Client.Services;
using MyBlazor.Client.Helper;

namespace MyBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddServices(builder.HostEnvironment);

            await builder.Build().RunAsync();
        }


    }

    public static class Extension
    {
        public static void AddServices(this IServiceCollection services, IWebAssemblyHostEnvironment env)
        {
            services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(env.IsDevelopment() ? "http://localhost:5000/" : env.BaseAddress) });
            // services.AddSingleton(sp => new UowService());
            services.AddSingleton<UowService>();
            services.AddSingleton<MyConsole>();

            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.TopRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                // config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });
        }
    }
}
