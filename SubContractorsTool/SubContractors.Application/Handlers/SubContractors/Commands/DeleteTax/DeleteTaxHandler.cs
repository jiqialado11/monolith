using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor.Tax;

namespace SubContractors.Application.Handlers.SubContractors.Commands.DeleteTax
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeleteTaxHandler : IRequestHandler<DeleteTax, Result<Unit>>
    {
        private readonly ISqlRepository<Tax, int> _taxSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaxHandler(ISqlRepository<Tax, int> taxSqlRepository, IUnitOfWork unitOfWork)
        {
            _taxSqlRepository = taxSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteTax request, CancellationToken cancellationToken)
        {
            var tax = await _taxSqlRepository.GetAsync(x => x.Id == request.Id, Array.Empty<string>() );
            if (tax == null)
            {
                return Result.NotFound($"Tax wasn't found in database with provided identifier {request.Id}");
            }

            await _taxSqlRepository.DeleteAsync(tax.Id);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
