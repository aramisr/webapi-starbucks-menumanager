using Core.mediatOR.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Starbucks.MenuManager.API.Application.Abstractions;
using Starbucks.MenuManager.API.Application.Coffes.DTOs;
using Starbucks.MenuManager.API.Domain.Entities;
using Starbucks.MenuManager.API.Persistence.Contexts;
using System;
using static Starbucks.MenuManager.API.Application.Coffes.Commands.CoffeCreate;

namespace Starbucks.MenuManager.API.Application.Coffes.Commands
{
    public class CoffeUpdate
    {
        public class Command : IRequest<Result>
        {
            public required Guid Id { get; set; }
            public required CoffeUpdateRequest CoffeUpdateRequest { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() 
            {
                RuleFor(x => x.CoffeUpdateRequest)
                .SetValidator(new RequestValidator());
            }
        }

        public class RequestValidator : AbstractValidator<CoffeUpdateRequest>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre no puede estar en blanco");

                RuleFor(x => x.Description)
               .NotEmpty()
               .WithMessage("La descripcion no puede estar en blanco");

                RuleFor(x => x.CategoryId)
               .NotEmpty()
               .WithMessage("El categoryId no puede estar en blanco");
            }
        }
        public class Handler(StarbucksDbContext context) : IRequestHandler<Command, Result>
        {
            private readonly StarbucksDbContext _context = context;
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var coffe = await _context.Coffes
                            .FirstOrDefaultAsync(x => x.Id == request.Id);
                
                if(coffe is null)
                {
                    return Result.Failure(CoffeErrors.CoffeNoExiste);
                }

                var coffeNameExists = await _context.Coffes.AnyAsync(x => x.Name == request.CoffeUpdateRequest.Name);

                if(coffeNameExists)
                {
                    return Result.Failure(CoffeErrors.NombreDuplicado);
                }

                coffe.Name = request.CoffeUpdateRequest.Name;
                coffe.Description = request.CoffeUpdateRequest.Description;
                coffe.CategoryId = request.CoffeUpdateRequest.CategoryId;
                coffe.Price = request.CoffeUpdateRequest.Price;

                context.Entry(coffe).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Result.Success();
            }
        }
    }
}
