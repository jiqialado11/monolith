using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Common.Redis;
using SubContractors.Domain.Common;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Commands.UpdateSubContractor
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]

    public class UpdateSubContractorHandler : IRequestHandler<UpdateSubContractor, Result<Unit>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Office, int> _subContractorOfficeSqlRepository;
        private readonly ISqlRepository<Domain.Common.Location, int> _locationSqlRepository;
        private readonly ISqlRepository<Market, int> _marketSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSubContractorHandler(ISqlRepository<SubContractor, int> subContractorSqlRepository,
            ISqlRepository<Office, int> subContractorOfficeSqlRepository,
            ISqlRepository<Location, int> locationSqlRepository,
            ISqlRepository<Market, int> marketSqlRepository,
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository,
            IUnitOfWork unitOfWork)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _subContractorOfficeSqlRepository = subContractorOfficeSqlRepository;
            _locationSqlRepository = locationSqlRepository;
            _marketSqlRepository = marketSqlRepository;
            _staffSqlRepository = staffSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateSubContractor request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.Id,
                new string[] { nameof(SubContractor.Location), nameof(SubContractor.Offices), nameof(SubContractor.AccountManager), nameof(SubContractor.Markets) });
            if (subContractor == null)
            {
                return Result.NotFound($"SubContractor wasn't found in database with provided identifier {request.Id}");
            }

            var location = await _locationSqlRepository.GetAsync(x => x.Id == request.LocationId);
            if (location == null)
            {
                return Result.NotFound($"Location wasn't found in database with provided identifier {request.LocationId}");
            }

            subContractor.Update(
                request.Name,
                (SubContractorType)request.SubContractorType,
                (SubContractorStatus)request.SubContractorStatus,
                location,
                request.Comment,
                request.LastInteractionDate.Value,
                request.IsNdaSigned.Value,
                request.Skills,
                request.Contact,
                request.CompanySite,
                request.Materials);

            var officeIds = subContractor.Offices.Select(o => o.Id).ToList();
            var marketIds = subContractor.Markets.Select(m => m.Id).ToList();

            if (!officeIds.Any() || !officeIds.Contains(request.SalesOfficeId.Value))
            {
                var salesOffice = await _subContractorOfficeSqlRepository.GetAsync(x => x.Id == request.SalesOfficeId);
                if (salesOffice == null)
                {
                    return Result.NotFound($"sales office wasn't found in database with provided identifier {request.SalesOfficeId}");
                }

                subContractor.RemoveOffice(
                    subContractor.Offices.FirstOrDefault(o => o.OfficeType == OfficeType.SalesOffice));
                subContractor.AddOffice(salesOffice);
            }

            if (!officeIds.Any() || !officeIds.Contains(request.DevelopmentOfficeId.Value))
            {
                var developmentOffice = await _subContractorOfficeSqlRepository.GetAsync(x => x.Id == request.DevelopmentOfficeId);
                if (developmentOffice == null)
                {
                    return Result.NotFound($"development office wasn't found in database with provided identifier {request.DevelopmentOfficeId}");
                }

                subContractor.RemoveOffice(
                    subContractor.Offices.FirstOrDefault(o => o.OfficeType == OfficeType.DevelopmentOffice));
                subContractor.AddOffice(developmentOffice);
            }

            if (!marketIds.Any() || !marketIds.Contains(request.MarketId.Value))
            {
                var market = await _marketSqlRepository.GetAsync(x => x.Id == request.MarketId);
                if (market == null)
                {
                    return Result.NotFound($"market wasn't found in database with provided identifier {request.MarketId}");
                }

                subContractor.RemoveMarket(
                    subContractor.Markets.FirstOrDefault());
                subContractor.AddMarket(market);
            }

            if (subContractor.AccountManager?.Id != request.AccountManagerId)
            {
                if (request.AccountManagerId == null)
                {
                    subContractor.DeAssignAccountManager();
                }
                else
                {
                    var accountManager = await _staffSqlRepository.GetAsync(request.AccountManagerId.Value, Array.Empty<string>() );
                    if (accountManager == null)
                    {
                        return Result.NotFound($"Account manager wasn't found in database with provided identifier {request.AccountManagerId}");
                    }

                    subContractor.AssignAccountManager(accountManager);
                }
            }

            await _subContractorSqlRepository.UpdateAsync(subContractor);
            await _unitOfWork.SaveAsync();            

            return Result.Success(ResultType.Accepted);
        }
    }
}
