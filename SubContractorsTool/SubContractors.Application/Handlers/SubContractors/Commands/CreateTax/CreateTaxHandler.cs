using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using SubContractors.Domain.SubContractor.Tax;

namespace SubContractors.Application.Handlers.SubContractors.Commands.CreateTax
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class CreateTaxHandler : IRequestHandler<CreateTax, Result<int>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Tax, int> _taxSqlRepository;
        private readonly ISqlRepository<TaxType, int> _taxTypeSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaxHandler(
            ISqlRepository<SubContractor, int> subContractorSqlRepository, 
            ISqlRepository<Tax, int> taxSqlRepository, 
            IUnitOfWork unitOfWork, 
            ISqlRepository<TaxType, int> taxTypeSqlRepository)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _taxSqlRepository = taxSqlRepository;
            _unitOfWork = unitOfWork;
            _taxTypeSqlRepository = taxTypeSqlRepository;
        }

        public async Task<Result<int>> Handle(CreateTax request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.SubContractorId, new string[] {});
            if (subContractor == null)
            {
                return Result.NotFound<int>($"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
            }

            var taxType = await _taxTypeSqlRepository.GetAsync(x => x.Id == request.TaxTypeId, Array.Empty<string>() );
            if (taxType == null)
            {
                return Result.NotFound<int>($"Tax type wasn't found in database with provided identifier {request.TaxTypeId}");
            }

            var tax = new Tax();
            tax.Create(request.Name, request.TaxNumber, request.Url, request.Date.Value, taxType, subContractor);

            await _taxSqlRepository.AddAsync(tax);
            await _unitOfWork.SaveAsync();

            return Result.Success(ResultType.Created, data: tax.Id);
        }
    }
}
