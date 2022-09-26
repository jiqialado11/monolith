using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.Staff.Queries.GetStaffListQuery
{
    [RequestLogging]
    [RequestValidation]
    [RequestCashing]
    public class GetStaffListQueryHandler : IRequestHandler<GetStaffListQuery, Result<IList<GetStaffListDto>>>
    {
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _sqlRepository;
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly IMapper _mapper;
        public GetStaffListQueryHandler(
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> sqlRepository,
            ISqlRepository<SubContractor, int> subContractorSqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _subContractorSqlRepository = subContractorSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetStaffListDto>>> Handle(GetStaffListQuery request, CancellationToken cancellationToken)
        {
            IList<GetStaffListDto> result;
            IEnumerable<Domain.SubContractor.Staff.Staff> staffs;

            if (request.SubContractorId.HasValue)
            {
                var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.SubContractorId.Value);
                if (subContractor == null)
                {
                    return Result.NotFound<IList<GetStaffListDto>>($"subcontractor wasn't found in database with provided identifier {request.SubContractorId}");
                }


                var queryResult = await _sqlRepository.FindAsync(
                    x => x.SubContractors.Contains(subContractor),
                    new[] { nameof(Domain.SubContractor.Staff.Staff.SubContractors) });

                 staffs= queryResult.AsEnumerable();

                
                if (!staffs.Any())
                {
                    return Result.NotFound<IList<GetStaffListDto>>($"Couldn't find entities related with provided subcontractor identifier {request.SubContractorId.Value}");
                }
                result = staffs.Select(s => _mapper.Map<GetStaffListDto>(s)).ToList();

                return Result.Ok(value: result);
            }
            staffs = (await _sqlRepository.FindAsync(x => true)).AsEnumerable();
            
            if (!staffs.Any())
            {
                return Result.NotFound<IList<GetStaffListDto>>("Couldn't find entities with provided parameters");
            }
            result = staffs.Select(s => _mapper.Map<GetStaffListDto>(s)).ToList();

            return Result.Ok(value: result);


        }
    }
}