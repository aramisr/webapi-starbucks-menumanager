using Core.Mappy.Interfaces;
using Core.mediatOR.Contracts;
using Starbucks.MenuManager.API.Application.Coffes.DTOs;
using Starbucks.MenuManager.API.Persistence.Contexts;

namespace Starbucks.MenuManager.API.Application.Coffes.Commands
{
    public class CoffeCreate
    {
        public class Command : IRequest<Guid> 
        {
            public required CoffeCreateRequest CoffeCreateRequest { get; set; }
        }

        public class Handler(StarbucksDbContext context, IMapper mapper) 
        : IRequestHandler<Command, Guid>
        {
            private readonly StarbucksDbContext _context = context;
            private readonly IMapper _mapper = mapper;
            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken) 
            {
                var coffe = _mapper.Map<Domain.Entities.Coffe>(request.CoffeCreateRequest);
                _context.Add(coffe);
                await _context.SaveChangesAsync(cancellationToken);

                return coffe.Id;
            }
        }
    }
}
