using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Agreement;

namespace SubContractors.Application.Handlers.Agreement.Commands.Delete_Addendum
{

    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeleteAddendumHandler : IRequestHandler<DeleteAddendum, Result<Unit>>
    {
        private readonly ISqlRepository<Addendum, int> _sqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddendumHandler(ISqlRepository<Addendum, int> sqlRepository, IUnitOfWork unitOfWork)
        {
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteAddendum request, CancellationToken cancellationToken)
        {
            var addendum = await _sqlRepository.GetAsync(request.Id.Value, new string[] { });
            if (addendum == null)
            {
                return Result.NotFound($"Addendum wasn't found in database with provided identifier {request.Id}");
            }

            await _sqlRepository.DeleteAsync(addendum.Id);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
