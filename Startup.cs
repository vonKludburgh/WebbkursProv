using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebbkursProv.Pages.IndexModel;

namespace WebbkursProv
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
            services.AddRazorPages();

            // Gör att vi kan använda oss av vår Gateway
            services.AddHttpClient<Gateway.MaxGateway>();            
            services.AddTransient<Models.IMaxGateway, Gateway.MaxGateway>();


            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireAdministratorRole",
            //         policy => policy.RequireRole("Admin"));
            //});

            // Kontrollerar auktoriseringen av den som försöker ta sig in på /admin
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/Privacy", "RequireAdministratorRole");
                options.Conventions.AuthorizeFolder("/Admin");
                options.Conventions.AllowAnonymousToPage("/Admin/Info");
                options.Conventions.AllowAnonymousToFolder("/Admin/PublicAdmin");
            });

            // håller reda på handle av den som är inloggad
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = ContextBoundObject => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.Add(
                    new PageRouteTransformerConvention(
                        new SlugifyParameterTransformer()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
