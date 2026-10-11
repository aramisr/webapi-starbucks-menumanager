using Core.Mappy.Interfaces;
using Core.mediatOR.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Starbucks.MenuManager.API.Application.Abstractions;
using Starbucks.MenuManager.API.Application.Coffes.DTOs;
using Starbucks.MenuManager.API.Domain.Entities;
using Starbucks.MenuManager.API.Persistence.Contexts;

namespace Starbucks.MenuManager.API.Application.Coffes.Commands
{
    public class CoffeCreate
    {
        public class Command : IRequest<Result<Guid>> 
        {
            public required CoffeCreateRequest CoffeCreateRequest { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CoffeCreateRequest).SetValidator(new RequestValidator());
            }
        }

        public class RequestValidator : AbstractValidator<CoffeCreateRequest> 
        { 
            public RequestValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido");
                RuleFor(x => x.Description).NotEmpty().WithMessage("La descripcion es requerida");
                RuleFor(x => x.CategoryId).NotEmpty().WithMessage("La categoría es requerida");
            }
        }

        public class Handler(StarbucksDbContext context, IMapper mapper) 
            : IRequestHandler<Command, Result<Guid>>
        {
            private readonly StarbucksDbContext _context = context;
            private readonly IMapper _mapper = mapper;
            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken) 
            {
                var nombreCafeIfExists = await _context.Coffes.AnyAsync(
                                                            x => x.Name == request.CoffeCreateRequest.Name
                                                         );
                if (nombreCafeIfExists)
                {
                    return Result<Guid>.Failure(
                       CoffeErrors.NombreDuplicado
                    );
                }
                var coffe = _mapper.Map<Domain.Entities.Coffe>(request.CoffeCreateRequest);
                _context.Add(coffe);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Success(coffe.Id);
            }
        }
    }
}
