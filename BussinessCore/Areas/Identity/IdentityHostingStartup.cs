﻿using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SmartClickCore.Areas.Identity.IdentityHostingStartup))]
namespace SmartClickCore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}