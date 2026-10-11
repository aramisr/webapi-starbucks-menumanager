using Starbucks.MenuManager.API.Domain.Abstractions;

namespace Starbucks.MenuManager.API.Domain.Entities
{
    public class Coffe : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? Imagen { get; set; }
        public Category? Category { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; } = [];
        public ICollection<CoffeIngredient> CoffeIngredients { get; set; } = [];
    }

    public static class CoffeErrors
    {
        public static Error NombreDuplicado = new Error
        (
            "COFFE.NOMBRE_DUPLICADO",
            "El nombre del cafe ya existe en el sistema"
        );
    }
}
