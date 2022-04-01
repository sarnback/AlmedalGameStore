using AlmedalGameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace AlmedalGameStore.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        // I Konstruktorn får vi options som vi passar till vår base class (db context)
        //För alla models man skapar till sin db så behöver man skapa en db_set i applicationDbCOntext
        //Hur? public DbSet<ModelName>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //Genres blir namnet på vårt table
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet <Cart> Carts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
