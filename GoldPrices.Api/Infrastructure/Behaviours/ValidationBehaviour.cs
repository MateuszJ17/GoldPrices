using FluentValidation;
using MediatR;

namespace GoldPrices.Infrastructure.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(
    List<IValidator<TRequest>> validators,
    ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            logger.LogInformation("Validating request {Request}", request);
            var validationContext = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                validators.Select(x => x.ValidateAsync(validationContext, cancellationToken)));
            var validationFailures = validationResults.Where(x => !x.IsValid).ToList();

            if (validationFailures.Count != 0)
            {
                logger.LogError("Request {Request} is not valid.", request);
                throw new ValidationException(validationFailures.SelectMany(x => x.Errors));
            }
        }

        return await next.Invoke();
    }
}