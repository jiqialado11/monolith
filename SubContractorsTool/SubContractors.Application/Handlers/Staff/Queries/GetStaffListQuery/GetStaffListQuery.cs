using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Redis;

namespace SubContractors.Application.Handlers.Staff.Queries.GetStaffListQuery
{
    public class GetStaffListQuery : IRequest<Result<IList<GetStaffListDto>>>, ICacheableRequest
    {
        public int? SubContractorId { get; set; }

        public string GetDomainIdentifier()
        {
            return typeof(Domain.SubContractor.Staff.Staff).FullName;
        }
    }

    public class GetStaffListValidator : AbstractValidator<GetStaffListQuery>
    {
        public GetStaffListValidator()
        { }
    }
}