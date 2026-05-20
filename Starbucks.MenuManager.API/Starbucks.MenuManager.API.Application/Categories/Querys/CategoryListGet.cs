using Microsoft.EntityFrameworkCore;
using Core.mediatOR.Contracts;
using Starbucks.MenuManager.API.Domain.Entities;
using Starbucks.MenuManager.API.Persistence.Contexts;

namespace Starbucks.MenuManager.API.Application.Categories.Querys
{
    public class CategoryListGet
    {
        public class Query : IRequest<List<Category>>
        {}

        public class Handler(StarbucksDbContext context) : IRequestHandler<Query, List<Category>>
        {
            private readonly StarbucksDbContext _context = context;
            public async Task<List<Category>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Categories.ToListAsync();
            }
        }
    }
}
