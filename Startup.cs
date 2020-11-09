using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ColorAPI.Entityframework;
using ColorAPI.ProductionServerMigration;

namespace ColorAPI
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
            var server=Configuration["DBServer"] ?? "192.168.10.200";
            var port=Configuration["DBPort"] ?? "1433";
            var user=Configuration["DBUser"] ?? "Suchi";
            var password=Configuration["DBPassword"] ?? "Kista@2020";
            var database=Configuration["database"] ?? "Colors";
            services.AddDbContext<ColorContext>(Temp=>Temp.UseSqlServer($"server={server},{port};initial catalog={database};user id={user};password={password}"));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            RuntimeMigration.Creation(app);
        }
    }
}
