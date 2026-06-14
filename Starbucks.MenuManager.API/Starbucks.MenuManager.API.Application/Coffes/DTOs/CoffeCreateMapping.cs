using Core.Mappy.Interfaces;
using Starbucks.MenuManager.API.Domain.Entities;

namespace Starbucks.MenuManager.API.Application.Coffes.DTOs
{
    public class CoffeCreateMapping : IMappingProfile
    {
        public void Configure(IMapper mapper)
        {
            mapper.CreateMap<CoffeCreateRequest, Coffe>();
        }
    }
}
