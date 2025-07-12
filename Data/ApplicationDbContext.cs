using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TequliasResturant.Models;
namespace TequliasResturant.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Define composite key and relationships for productIngredient
            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });
            modelBuilder.Entity<ProductIngredient>()
                .HasOne(Pi => Pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);
            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);
            //Seed Data
            modelBuilder.Entity<Category>().HasData(
                new Category { CaregoryId = 1, Name = "Appetizer" },
                new Category { CaregoryId = 2, Name = "Entree" },
                new Category { CaregoryId = 3, Name = "Side Dish" },
                new Category { CaregoryId = 4, Name = "Dessert" },
                new Category { CaregoryId = 5, Name = "Beverage" }
           );
            modelBuilder.Entity<Ingredient>().HasData(
                //add mexican restaurant ingredients here
                new Ingredient { IngredientId = 1, Name = "Beef" },
                new Ingredient { IngredientId = 2, Name = "Checken" },
                new Ingredient { IngredientId = 3, Name = "Fish" },
                new Ingredient { IngredientId = 4, Name = "Tortilla" },
                new Ingredient { IngredientId = 5, Name = "Lettuce" },
                new Ingredient { IngredientId = 6, Name = "Tomato" }
           );
            modelBuilder.Entity<Product>().HasData(
                //Add mexican restaurent food entries here
                new Product
                {
                    ProductId = 1,
                    Name = "Beef Taco",
                    Descrition ="A delicious beef taco",
                    Price = 2.50m,
                    Stock = 100,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Checken Taco",
                    Descrition = "A delicious chicken taco",
                    Price = 1.99m,
                    Stock = 101,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Fish Taco",
                    Descrition = "A delicious fish taco",
                    Price = 3.99m,
                    Stock = 90,
                    CategoryId = 2
                }

                );
            modelBuilder.Entity<ProductIngredient>().HasData(
               new ProductIngredient { ProductId = 1, IngredientId = 1 },
               new ProductIngredient { ProductId = 1, IngredientId = 4 },
               new ProductIngredient { ProductId = 1, IngredientId = 5 },
               new ProductIngredient { ProductId = 1, IngredientId = 6 },
               new ProductIngredient { ProductId = 2, IngredientId = 2 },
               new ProductIngredient { ProductId = 2, IngredientId = 4 },
               new ProductIngredient { ProductId = 2, IngredientId = 5 },
               new ProductIngredient { ProductId = 2, IngredientId = 6 },
               new ProductIngredient { ProductId = 3, IngredientId = 3 },
               new ProductIngredient { ProductId = 3, IngredientId = 4 },
               new ProductIngredient { ProductId = 3, IngredientId = 5 },
               new ProductIngredient { ProductId = 3, IngredientId = 6 }
               );

        }
    }
}
