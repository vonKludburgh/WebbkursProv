using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebbkursProv.Areas.Identity.Data;
using WebbkursProv.Data;

[assembly: HostingStartup(typeof(WebbkursProv.Areas.Identity.IdentityHostingStartup))]
namespace WebbkursProv.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WebbkursProvContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WebbkursProvContextConnection")));

                //services.AddDefaultIdentity<WebbkursProvUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<WebbkursProvContext>();

                services.AddDefaultIdentity<WebbkursProvUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<WebbkursProvContext>();

            });
        }
    }
}