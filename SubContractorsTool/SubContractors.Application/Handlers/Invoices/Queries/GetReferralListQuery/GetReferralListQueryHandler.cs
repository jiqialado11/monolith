using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using StaffModel = SubContractors.Domain.SubContractor.Staff.Staff;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetReferralListQuery
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class GetReferralListQueryHandler : IRequestHandler<GetReferralListQuery, Result<List<GetReferralListDto>>>
    {
        private readonly ISqlRepository<StaffModel, int> _staffSqlRepository;
        private readonly IMapper _mapper;

        public GetReferralListQueryHandler(ISqlRepository<StaffModel, int> sqlRepository,
                                           IMapper mapper)
        {
            _mapper = mapper;
            _staffSqlRepository = sqlRepository;
        }

        public async Task<Result<List<GetReferralListDto>>> Handle(GetReferralListQuery request, CancellationToken cancellationToken)
        {
            var filferDate = DateTime.Today.AddDays(-90);
            Expression<Func<StaffModel, bool>> predicate = null;
            if (request.PaymentNumber == 1)
            {
                predicate = (x) => x.StartDate >= filferDate && x.Invoices.Count == 0;
            }
            if (request.PaymentNumber == 2)
            {
                predicate = (x) => x.Invoices.Count == 1 && x.Invoices.FirstOrDefault(p => p.PaymentNumber == 1) != null;
            }

            var staffCollection = await _staffSqlRepository.FindAsync(predicate, new string[]
                                        { 
                                            nameof(StaffModel.Invoices)
                                        });

            var result = staffCollection.Select(s => _mapper.Map<GetReferralListDto>(s)).ToList();

            return Result.Ok(value: result);
        }
    }
}
