using AspNet31.SimpleInjector.HostedService.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace AspNet31.SimpleInjector.HostedService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _container.Options.ResolveUnregisteredConcreteTypes = true;
        }

        public IConfiguration Configuration { get; }
        private readonly Container _container = new Container();


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddControllers();
            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore();
                options.AddLogging();
            });
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseSimpleInjector(_container);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseMiddleware<LivenessMiddleware>(_container);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            _container.Verify();
        }
    }
}
