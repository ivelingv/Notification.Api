using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Giftshop.Core.Extensions;
using Giftshop.Core.Messaging;
using Giftshop.Notifications.Hub;
using Giftshop.Notifications.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Giftshop.Notifications
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors()
                .AddJwtServices(Configuration)
                .AddMessaging(Configuration, typeof(ProductCreatedConsumer))
                .AddMonitoring(Configuration)
                .AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options
                    .WithOrigins("http://localhost:55000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());

            app.UseAuthentication();
            app.UseSignalR(config =>
            {
                config.MapHub<NotificationHub>("/notifications");
            });

            app.UseMonitoring();
        }
    }
}
