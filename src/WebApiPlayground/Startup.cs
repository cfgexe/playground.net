using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Net.Http;
using WebApiPlayground.HttpClientDemo;

namespace WebApiPlayground
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region Do Not Use Client This Way

        private async void DoNotUseItThisWayClient()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync("https://example.com");
            }
        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure HttpClients.
            services.AddHttpClient();
            //ConfigureNamedHttpClient(services);
            //ConfigureTypedHttpClient1(services);
            //ConfigureTypedHttpClient2(services);

            services.AddMvc();
        }

        public void ConfigureNamedHttpClient(IServiceCollection services)
        {
            services.AddHttpClient(NamedHttpClients.MyJob, client =>
            {
                client.BaseAddress = new Uri("http://sampleaspnetcorewebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Add("User-Agent", "KROS Backup Service");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json; charset=utf-8");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { Proxy = GetProxy() });

            services.AddHttpClient(NamedHttpClients.MyJobV2, client =>
            {
                client.BaseAddress = new Uri("http://sampleaspnetcorewebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Add("User-Agent", "KROS Backup Service");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { Proxy = GetProxy() });
        }

        public void ConfigureTypedHttpClient1(IServiceCollection services)
        {
            services.AddHttpClient<MyJobClient>(client =>
            {
                client.BaseAddress = new Uri("http://sampleaspnetcorewebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Add("User-Agent", "KROS Backup Service");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { Proxy = GetProxy() });
        }

        public void ConfigureTypedHttpClient2(IServiceCollection services)
        {
            services.AddHttpClient<IMyJobClient, MyJobClientV2>(client =>
            {
                client.BaseAddress = new Uri("http://sampleaspnetcorewebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Add("User-Agent", "KROS Backup Service");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { Proxy = GetProxy() });
        }

        private static IWebProxy GetProxy() => new WebProxy("192.168.1.3:3128", true);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
        }
    }
}
