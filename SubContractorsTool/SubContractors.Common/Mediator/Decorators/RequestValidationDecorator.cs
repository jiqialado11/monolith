using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Common.Mediator.Decorators
{
    public class RequestValidationDecorator<TRequest, TResult> : IPipelineBehavior<TRequest, Result<TResult>> where TRequest : IRequest<Result<TResult>>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationDecorator(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<Result<TResult>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResult>> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators.Select(v => v.Validate(context))
                                      .SelectMany(result => result.Errors)
                                      .Where(f => f != null)
                                      .ToList();

            if (failures.Count != 0)
            {
                var dictionary = new Dictionary<string, List<string>>();

                foreach (var failure in failures)
                {
                    if (dictionary.ContainsKey(failure.PropertyName))
                    {
                        continue;
                    }

                    var errorList = new List<string>();
                    errorList.AddRange(failures.Where(x => x.PropertyName == failure.PropertyName)
                                               .Select(x => x.ErrorMessage)
                                               .ToList());

                    dictionary.Add(failure.PropertyName, errorList);
                }

                return await Task.FromResult(Result.Error<TResult>(ResultType.UnsupportedMediaType, "Validation failure", dictionary));
            }

            return await next();
        }
    }
}