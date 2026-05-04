using Microsoft.EntityFrameworkCore;
using Starbucks.MenuManager.API.Domain.Entities;
using System.Security.Cryptography;

namespace Starbucks.MenuManager.API.Persistence.Contexts
{
    public class StarbucksDbContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<Category> Categories { get; set; }
        public required DbSet<Coffe> Coffes { get; set; }
        public required DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Coffes)
                .WithOne(co => co.Category)
                .HasForeignKey(co => co.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Coffe>()
            .Property(co => co.Price)
            .HasPrecision(10, 2);

            modelBuilder.Entity<Coffe>()
            .HasMany(ing => ing.Ingredients)
            .WithMany(ing => ing.Coffes)
            .UsingEntity<CoffeIngredient>(
                j => j
                     .HasOne(p => p.Ingredient)
                     .WithMany(p => p.CoffeIngredients)
                     .HasForeignKey(p => p.IngredientId),
                j => j
                     .HasOne(p => p.Coffe)
                     .WithMany(p => p.CoffeIngredients)
                     .HasForeignKey(p => p.CoffeId),
                j =>
                {
                    j.HasKey(t => new { t.CoffeId, t.IngredientId });//Primary key compuesto conformado por ambos ID
                }
             );

            modelBuilder.Entity<Category>().HasData(GetCategories());
        }

        private IEnumerable<Category> GetCategories()
        {
            //Dentro de este arreglo Enum.GetValues<CategoryEnum>()
            //El select va tomar cada elemento del arreglo y por cada elemento devolvera un objeto de tipo Category
            return Enum.GetValues<CategoryEnum>().Select(p => Category.Create((int)p));
            
        }
    }
}
