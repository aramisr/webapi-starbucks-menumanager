using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbucks.MenuManager.API.Domain.Entities
{
    public class Category
    {
        [SetsRequiredMembers]
        private Category(int id, string name) => (Id, Name) = (id, name);
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        //Colections of coffes
        public ICollection<Coffe> Coffes { get; set; } = [];
        //Este metodo devuelve un objeto category que obtiene el nombre y id de la categoria
        public static Category Create(int id)
        {
            var categoryName = (CategoryEnum)id;
            string categoryNameString = categoryName.ToString();

            //Llamar a la instancia del constructor
            return new Category(id, categoryNameString);
        }
    }
}
