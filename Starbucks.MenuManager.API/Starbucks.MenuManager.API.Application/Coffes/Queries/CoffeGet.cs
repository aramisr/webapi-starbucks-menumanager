using Core.Mappy.Interfaces;
using Core.mediatOR.Contracts;
using Microsoft.EntityFrameworkCore;
using Starbucks.MenuManager.API.Application.Abstractions;
using Starbucks.MenuManager.API.Application.Coffes.DTOs;
using Starbucks.MenuManager.API.Persistence.Contexts;

namespace Starbucks.MenuManager.API.Application.Coffes.Queries
{
    public class CoffeGet
    {
        public class Query : IRequest<Result<List<CoffeResponse>>>
        { }

        public class Handler(StarbucksDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<CoffeResponse>>>
        {
            private readonly StarbucksDbContext _context = context;
            private readonly IMapper _mapper = mapper;
            public async Task<Result<List<CoffeResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var coffes = await _context.Coffes.ToListAsync();
                var coffeResponse = _mapper.Map<List<CoffeResponse>>(coffes);

                return Result<List<CoffeResponse>>.Success(coffeResponse);
            }
        }
    }
}
