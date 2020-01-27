using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CustomersApi.Repositories;
using CustomersApi.Services;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;

namespace CustomersApi
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
            services.AddControllers();
            services.AddHttpClient();
            
            services.AddSingleton<ITracer>(serviceProvider =>  
            {  
                string serviceName = Assembly.GetEntryAssembly()?.GetName().Name;  
  
                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();  
  
                ISampler sampler = new ConstSampler(sample: true);  
  
                ITracer tracer = new Tracer.Builder(serviceName)  
                    .WithLoggerFactory(loggerFactory)  
                    .WithSampler(sampler)  
                    .Build();  
  
                GlobalTracer.Register(tracer);  
  
                return tracer;  
            });

            services.AddSingleton<CustomersRepository>();
            services.AddSingleton<ICustomersRepository>(provider =>
                new TracedCustomersRepository(
                    provider.GetRequiredService<CustomersRepository>(),
                    provider.GetRequiredService<ITracer>()));

            services.AddScoped<ICustomersService, CustomersService>();      
            
            services.AddOpenTracing();  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}