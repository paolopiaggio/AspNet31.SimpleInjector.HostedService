using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleApp.Middlewares;
using SimpleInjector;
using System;

namespace SampleApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly Container _container = new Container();

        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Configure Service started");
            services.AddLogging();
            services.AddControllers();
            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore();
                options.AddLogging();
                options.AddHostedService<MyHostedService>();
            });
            Console.WriteLine("Configure Service completed");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("Configure App started");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseMiddleware<LivenessMiddleware>(_container);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            Console.WriteLine("Configure App completed");
        }
    }
}
