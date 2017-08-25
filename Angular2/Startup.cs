using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VisualStudio2017.Backend.Data;

namespace VisualStudio2017.Angular4
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
	        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
	        services.AddScoped<ISessionWrapper, MySessionWrapper>();
	        services.AddScoped<IAppDataAccess, HttpSessionDataAccess>();
	        services.AddScoped<IAppDataContext, MongoDBContext>();

	        // Add framework services.
	        services.AddMvc();
	        services.AddMemoryCache();
	        services.AddSession(options =>
	        {
		        // Set a short timeout for easy testing.
		        options.IdleTimeout = TimeSpan.FromMinutes(30);
		        options.CookieHttpOnly = false;
		        options.CookieName = "_some_stupid_name_";
	        });
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
	        app.UseSession();
			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
