using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using ColorAPI.Entityframework;
using ColorAPI.Models;

namespace ColorAPI.ProductionServerMigration
{
    public static class RuntimeMigration
    { 
        public static void Creation(IApplicationBuilder builder)
        {
            using(var serviceScope = builder.ApplicationServices.CreateScope()){
                SeedData(serviceScope.ServiceProvider.GetService<ColorContext>());
            }
        }

        public static void SeedData(ColorContext context)
        {
            Console.WriteLine("Applying Migration");
            context.Database.Migrate();
            if(!context.colors.Any())
            {
                Console.WriteLine("Seeding");
                context.colors.AddRange(new Color(){
                    colorName = "Red"
                },
                new Color(){
                    colorName = "Yellow"
                },
                new Color(){
                    colorName = "Green"
                }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Data Available");
            }
        }
    }
}