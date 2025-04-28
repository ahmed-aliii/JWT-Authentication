using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPIAssginment.Models.Data
{
    public class WebAPIContext : IdentityDbContext<ApplicationUser> //IdentityDbContext for Identity
    {
        public WebAPIContext():base() {}
        
        public WebAPIContext(DbContextOptions<WebAPIContext> options):base(options) { } //for DB connection


        //Dbsets
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           base.OnModelCreating(modelBuilder); //for Identity


            modelBuilder.Entity<Category>(entityBuilder =>
            {
                entityBuilder.HasData(
                    new Category() {Id=1 ,  Name = "Mobiles" },
                    new Category() {Id=2 ,  Name = "TVs" }
                    );
            });

            modelBuilder.Entity<Product>(entityBuilder =>
            {
                entityBuilder.HasData(
                    new Product() {Id=1, Name = "Iphone 11", Description = "Good Phone", Price = 1000, CategoryId = 1 },
                    new Product() {Id = 2, Name = "Galaxy S22 Ultra", Description = "Good Phone", Price = 9999, CategoryId = 1 },
                    new Product() {Id = 3, Name = "LG TV", Description = "Good Phone", Price = 1000, CategoryId = 2 },
                    new Product() {Id = 4, Name = "Samsung TV", Description = "Good Phone", Price = 1000, CategoryId = 2 }
                    );
            });
        }
    }
}
