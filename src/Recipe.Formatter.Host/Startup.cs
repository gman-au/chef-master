using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipe.Formatter.Adapters.SchemaOrg;
using Recipe.Formatter.Adapters.SchemaOrg.Factories;
using Recipe.Formatter.Infrastructure;
using Recipe.Formatter.Interfaces;

namespace Recipe.Formatter.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<IRecipeAdapter, SchemaOrgAdapter>()
                .AddTransient<IHtmlDownloader, HtmlDownloader>()
                .AddTransient<IJsonStripper, JsonStripper>()
                .AddTransient<IJsonParser, JsonParser>();

            services
                .AddTransient<IResponseFactory, ResponseFactory>()
                .AddTransient<IInstructionsFactory, InstructionsFactory>()
                .AddTransient<IImageFactory, ImageFactory>()
                .AddTransient<IYieldFactory, YieldFactory>()
                .AddTransient<ITimesFactory, TimesFactory>()
                .AddTransient<IResponseFormatter, ResponseFormatter>();

            services
                .AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .Configure<MyBuildConfiguration>(Configuration.GetSection("MyBuildConfiguration"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Status");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
