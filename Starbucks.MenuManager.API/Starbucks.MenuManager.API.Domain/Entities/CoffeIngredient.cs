namespace Starbucks.MenuManager.API.Domain.Entities
{
    public class CoffeIngredient
    {
        public Guid IngredientId { get; set; }
        public Guid CoffeId { get; set; }
        public Ingredient? Ingredient { get; set; }
        public Coffe? Coffe { get; set; }
    }
}
