
using FluentValidation;
using LanguageExt.Common;
using MediatR;

namespace Oss.Validators
{
    public class ValidationBehavior<TRequest, TResult>: IPipelineBehavior<TRequest, Result<TResult>>
    {
        private readonly IValidator<TRequest> validator_;
        private readonly ILogger logger_;

        public ValidationBehavior(IValidator<TRequest> validator, ILogger<ValidationBehavior<TRequest, TResult>> logger)
        {
            validator_ = validator;
            logger_ = logger;
        }

        public async Task<Result<TResult>> Handle(TRequest request, RequestHandlerDelegate<Result<TResult>> next, CancellationToken cancellationToken)
        {
            var result = await validator_.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errorMessage = string.Join(" ", result.Errors.Select(i => i.ErrorMessage));

                logger_.LogError(errorMessage);

                return new Result<TResult>(new Exception(errorMessage));
            }

            return await next();
        }
    }
}
