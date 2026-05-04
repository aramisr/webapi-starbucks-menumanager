using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbucks.MenuManager.API.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<Coffe> Coffes { get; set; } = [];
        public ICollection<CoffeIngredient> CoffeIngredients { get; set; } = [];
    }
}
