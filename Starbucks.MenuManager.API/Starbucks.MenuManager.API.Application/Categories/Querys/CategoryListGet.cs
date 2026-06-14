using Microsoft.EntityFrameworkCore;
using Core.mediatOR.Contracts;
using Starbucks.MenuManager.API.Persistence.Contexts;
using Starbucks.MenuManager.API.Application.Categories.DTOs;
using Core.Mappy.Interfaces;

namespace Starbucks.MenuManager.API.Application.Categories.Querys
{
    public class CategoryListGet
    {
        public class Query : IRequest<List<CategoryResponse>>
        { }

        public class Handler(
            StarbucksDbContext context,
            IMapper mapper
        ) 
        : IRequestHandler<Query, List<CategoryResponse>>
        {
            private readonly StarbucksDbContext _context = context;
            private readonly IMapper _mapper = mapper;
            public async Task<List<CategoryResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = await _context.Categories.ToListAsync();
                return _mapper.Map<List<CategoryResponse>>(categories);      
            }
        }
    }
}
