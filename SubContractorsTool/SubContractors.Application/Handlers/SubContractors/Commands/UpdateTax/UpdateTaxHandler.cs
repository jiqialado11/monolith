using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor.Tax;

namespace SubContractors.Application.Handlers.SubContractors.Commands.UpdateTax
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class UpdateTaxHandler : IRequestHandler<UpdateTax, Result<Unit>>
    {
        private readonly ISqlRepository<Tax, int> _taxSqlRepository;
        private readonly ISqlRepository<TaxType, int> _taxTypeSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaxHandler(ISqlRepository<Tax, int> taxSqlRepository, IUnitOfWork unitOfWork,
            ISqlRepository<TaxType, int> taxTypeSqlRepository)
        {
            _taxSqlRepository = taxSqlRepository;
            _unitOfWork = unitOfWork;
            _taxTypeSqlRepository = taxTypeSqlRepository;
        }

        public async Task<Result<Unit>> Handle(UpdateTax request, CancellationToken cancellationToken)
        {
            var tax = await _taxSqlRepository.GetAsync(x => x.Id == request.Id, Array.Empty<string>() );
            if (tax == null)
            {
                return Result.NotFound($"Tax wasn't found in database with provided identifier {request.Id}");
            }

            var taxType = await _taxTypeSqlRepository.GetAsync(x => x.Id == request.TaxTypeId, Array.Empty<string>() );
            if (taxType == null)
            {
                return Result.NotFound(
                    $"Tax type wasn't found in database with provided identifier {request.TaxTypeId}");
            }

            tax.Update(request.Name, request.TaxNumber, request.Url, request.Date.Value, taxType);

            await _taxSqlRepository.UpdateAsync(tax);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
