using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure HttpClients.
            //ConfigureSimpleHttpClient(services);
            //ConfigureNamedHttpClient(services);
            //ConfigureTypedHttpClient1(services);
            //ConfigureTypedHttpClient2(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void ConfigureSimpleHttpClient(IServiceCollection services)
        {
            services.AddHttpClient();
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
            });
            //.ConfigureHttpMessageHandlerBuilder(handlerBuilder => new HttpClientHandler() { Proxy = GetProxy() });

            services.AddHttpClient(NamedHttpClients.MyJobV2, client =>
            {
                client.BaseAddress = new Uri("http://sampleaspnetcorewebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Add("User-Agent", "KROS Backup Service");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json; charset=utf-8");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            });
            //.ConfigureHttpMessageHandlerBuilder(handlerBuilder => new HttpClientHandler() { Proxy = GetProxy() });
        }

        public void ConfigureTypedHttpClient1(IServiceCollection services)
        {
            services.AddHttpClient<MyJobClient>(client =>
            {
                client.BaseAddress = new Uri("http://sampleaspnetcorewebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Add("User-Agent", "KROS Backup Service");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json; charset=utf-8");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            });
        }

        public void ConfigureTypedHttpClient2(IServiceCollection services)
        {
            services.AddHttpClient<IMyJobClient, MyJobClientV2>(client =>
            {
                client.BaseAddress = new Uri("http://sampleaspnetcorewebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Add("User-Agent", "KROS Backup Service");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json; charset=utf-8");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip");
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            });
        }

        private static IWebProxy GetProxy() => null; // new WebProxy("192.168.1.3:3128", true);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
        }
    }
}
