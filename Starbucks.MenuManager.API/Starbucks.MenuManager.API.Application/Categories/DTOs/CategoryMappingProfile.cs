using Core.Mappy.Interfaces;
using Starbucks.MenuManager.API.Domain.Entities;

namespace Starbucks.MenuManager.API.Application.Categories.DTOs
{
    public class CategoryMappingProfile : IMappingProfile
    {
        public void Configure(IMapper mapper)
        {
            mapper.CreateMap<Category, CategoryResponse>(
                cfg =>
                {
                    cfg.Map(dest => dest.CategoryId, src => src.Id);
                    cfg.Map(dest => dest.NameTest, src => src.Name);
                }
            );
        }
    }
}
