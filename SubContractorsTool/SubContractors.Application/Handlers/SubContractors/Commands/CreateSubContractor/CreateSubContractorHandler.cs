using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Commands.CreateSubContractor
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class CreateSubContractorHandler : IRequestHandler<CreateSubContractor, Result<int>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Office, int> _subContractorOfficeSqlRepository;
        private readonly ISqlRepository<Domain.Common.Location, int> _locationSqlRepository;
        private readonly ISqlRepository<Market, int> _marketSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubContractorHandler(
            IUnitOfWork unitOfWork, 
            ISqlRepository<SubContractor, int> subContractorSqlRepository, 
            ISqlRepository<Office, int> subContractorOfficeSqlRepository, 
            ISqlRepository<Domain.Common.Location, int> locationSqlRepository, 
            ISqlRepository<Market, int> marketSqlRepository
            )
        {
            _unitOfWork = unitOfWork;
            _subContractorSqlRepository = subContractorSqlRepository;
            _subContractorOfficeSqlRepository = subContractorOfficeSqlRepository;
            _locationSqlRepository = locationSqlRepository;
            _marketSqlRepository = marketSqlRepository;
        }

        public async Task<Result<int>> Handle(CreateSubContractor request, CancellationToken cancellationToken)
        {
            var location = await _locationSqlRepository.GetAsync(x => x.Id == request.LocationId, Array.Empty<string>() );
            if (location == null)
            {
                return Result.NotFound<int>($"Location wasn't found in database with provided identifier {request.LocationId}");
            }
            

            var salesOffice = await _subContractorOfficeSqlRepository.GetAsync(x => x.Id == request.SalesOfficeId, Array.Empty<string>() );
            if (salesOffice == null)
            {
                return Result.NotFound<int>($"sales office wasn't found in database with provided identifier {request.SalesOfficeId}");
            }

            var developmentOffice = await _subContractorOfficeSqlRepository.GetAsync(x => x.Id == request.DevelopmentOfficeId, Array.Empty<string>() );
            if (developmentOffice == null)
            {
                return Result.NotFound<int>($"development office wasn't found in database with provided identifier {request.DevelopmentOfficeId}");
            }

            var market = await _marketSqlRepository.GetAsync(x => x.Id == request.MarketId, Array.Empty<string>() );
            if (market == null)
            {
                return Result.NotFound<int>($"market wasn't found in database with provided identifier {request.MarketId}");
            }

            var subContractor = new SubContractor();
            subContractor.Create(
                request.Name, 
                (SubContractorType) request.SubContractorType, 
                (SubContractorStatus) request.SubContractorStatus, 
                location, 
                request.Comment, 
                request.LastInteractionDate.Value, 
                request.IsNdaSigned.Value,
                request.Skills,
                request.Contact,
                request.CompanySite,
                request.Materials);
            
                subContractor.AddOffice(salesOffice);
                subContractor.AddOffice(developmentOffice);
                subContractor.AddMarket(market);
          
            await _subContractorSqlRepository.AddAsync(subContractor);
            await _unitOfWork.SaveAsync();

            return Result.Success(ResultType.Created, data: subContractor.Id);
        }
    }
}