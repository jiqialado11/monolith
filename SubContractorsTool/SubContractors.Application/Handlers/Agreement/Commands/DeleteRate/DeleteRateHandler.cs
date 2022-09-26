using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Agreement;

namespace SubContractors.Application.Handlers.Agreement.Commands.DeleteRate
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeleteRateHandler : IRequestHandler<DeleteRate, Result<Unit>>
    {
        private readonly ISqlRepository<Rate, int> _sqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRateHandler(ISqlRepository<Rate, int> sqlRepository, IUnitOfWork unitOfWork)
        {
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteRate request, CancellationToken cancellationToken)
        {
            var rate = await _sqlRepository.GetAsync(request.Id.Value, new string[] { });
            if (rate == null)
            {
                return Result.NotFound($"Rate wasn't found in database with provided identifier {request.Id}");
            }

            await _sqlRepository.DeleteAsync(rate.Id);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
