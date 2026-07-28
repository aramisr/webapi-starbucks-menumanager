using Core.mediatOR.Contracts;
using Core.MediatOR.Contracts;
using FluentValidation;
using Starbucks.MenuManager.API.Application.Exceptions;

namespace Starbucks.MenuManager.API.Application.Abstractions
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next
        )
        {
            if (!_validators.Any())
            {
                return await next();//Continuar flujo y continuar al Handler
            }

            var context = new ValidationContext<TRequest>(request);

            var validationErrors = _validators
                .Select(validators => validators.Validate(context))
                .Where(validationResult => validationResult.Errors.Any())
                .SelectMany(validationResult => validationResult.Errors)
                .Select(ValidationFailure => new ValidationError(
                    ValidationFailure.PropertyName,
                    ValidationFailure.ErrorMessage
                )).ToList();

            if (validationErrors.Any())
            {
                throw new Exceptions.ValidationException(validationErrors);
            }
            //Si no encuentra ningun tipo de error de validacion,
            //entonces que ejecute el handler
            return await next();
        }
    }
}
