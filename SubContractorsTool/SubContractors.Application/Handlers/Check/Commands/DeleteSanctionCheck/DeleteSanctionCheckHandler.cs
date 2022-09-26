using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Check;

namespace SubContractors.Application.Handlers.Check.Commands.DeleteSanctionCheck
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeleteSanctionCheckHandler : IRequestHandler<DeleteSanctionCheck, Result<Unit>>
    {
        private readonly ISqlRepository<SanctionCheck, int> _sqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSanctionCheckHandler(ISqlRepository<SanctionCheck, int> sqlRepository, IUnitOfWork unitOfWork)
        {
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteSanctionCheck request, CancellationToken cancellationToken)
        {
            var check = await _sqlRepository.GetAsync(request.Id.Value, Array.Empty<string>());
            if (check == null)
            {
                return Result.NotFound($"Sanction Check wasn't found in database with provided identifier {request.Id}");
            }

            await _sqlRepository.DeleteAsync(check.Id);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
