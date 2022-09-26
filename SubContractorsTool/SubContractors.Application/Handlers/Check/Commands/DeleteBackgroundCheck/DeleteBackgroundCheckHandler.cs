using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Check;

namespace SubContractors.Application.Handlers.Check.Commands.DeleteBackgroundCheck
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeleteBackgroundCheckHandler : IRequestHandler<DeleteBackgroundCheck, Result<Unit>>
    {
        private readonly ISqlRepository<BackgroundCheck, int> _sqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBackgroundCheckHandler(ISqlRepository<BackgroundCheck, int> sqlRepository, IUnitOfWork unitOfWork)
        {
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteBackgroundCheck request, CancellationToken cancellationToken)
        {
            var check = await _sqlRepository.GetAsync(request.Id.Value, Array.Empty<string>());
            if (check == null)
            {
                return Result.NotFound($"Background Check wasn't found in database with provided identifier {request.Id}");
            }

            await _sqlRepository.DeleteAsync(check.Id);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
