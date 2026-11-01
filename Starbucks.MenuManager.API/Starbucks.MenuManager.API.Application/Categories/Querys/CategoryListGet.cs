using Microsoft.EntityFrameworkCore;
using Core.mediatOR.Contracts;
using Starbucks.MenuManager.API.Persistence.Contexts;
using Starbucks.MenuManager.API.Application.Categories.DTOs;
using Core.Mappy.Interfaces;
using Starbucks.MenuManager.API.Application.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Starbucks.MenuManager.API.Application.Categories.Querys
{
    public class CategoryListGet
    {
        public class Query : IRequest<Result<List<CategoryResponse>>>
        { }

        public class Handler(
            StarbucksDbContext context,
            IMapper mapper
        ) 
        : IRequestHandler<Query, Result<List<CategoryResponse>>>
        {
            private readonly StarbucksDbContext _context = context;
            private readonly IMapper _mapper = mapper;
            public async Task<Result<List<CategoryResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = await _context.Categories.ToListAsync();
                var categoryResponse = _mapper.Map<List<CategoryResponse>>(categories); 
                
                return Result<List<CategoryResponse>>.Success(categoryResponse);
            }
        }
    }
}
