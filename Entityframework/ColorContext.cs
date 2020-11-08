using ColorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColorAPI.Entityframework
{
    public class ColorContext : DbContext
    {
        public ColorContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Color> colors {get; set;}
    }
}