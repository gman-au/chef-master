using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipe.Formatter.Adapters.Groq;
using Recipe.Formatter.Adapters.Ollama;
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
            var ollamaEndpoint = Environment.GetEnvironmentVariable("OLLAMA_ENDPOINT");
            var groqEndpoint = Environment.GetEnvironmentVariable("GROQ_ENDPOINT");
            var qristEndpoint = Environment.GetEnvironmentVariable("QRIST_ENDPOINT");

            services
                .AddTransient<IRecipeAdapter, SchemaOrgAdapter>();

            services
                .AddTransient<IHtmlDownloader, HtmlDownloader>()
                .AddTransient<IHtmlCleaner, HtmlCleaner>()
                .AddTransient<IJsonStripper, JsonStripper>()
                .AddTransient<IJsonParser, JsonParser>();

            services
                .AddTransient<IResponseFactory, ResponseFactory>()
                .AddTransient<IInstructionsFactory, InstructionsFactory>()
                .AddTransient<IImageFactory, ImageFactory>()
                .AddTransient<IYieldFactory, YieldFactory>()
                .AddTransient<ITimesFactory, TimesFactory>()
                .AddTransient<ISchemaGenerator, SchemaGenerator>()
                .AddTransient<IResponseFormatter, ResponseFormatter>();

            if (!string.IsNullOrEmpty(ollamaEndpoint))
            {
                services
                    .AddTransient<IOllamaRequestBuilder, OllamaRequestBuilder>()
                    .AddTransient<IRecipeAdapter, OllamaAdapter>();
            }

            if (!string.IsNullOrEmpty(groqEndpoint))
            {
                services
                    .AddTransient<IGroqRequestBuilder, GroqRequestBuilder>()
                    .AddTransient<IRecipeAdapter, GroqAdapter>();
            }

            if (!string.IsNullOrEmpty(qristEndpoint))
            {
                services
                    .AddTransient<IQristAdapter, QristAdapter>();
            }

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
