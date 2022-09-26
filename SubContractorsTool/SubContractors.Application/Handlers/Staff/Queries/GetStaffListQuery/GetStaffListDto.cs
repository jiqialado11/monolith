using System;

namespace SubContractors.Application.Handlers.Staff.Queries.GetStaffListQuery
{
    public class GetStaffListDto
    {
        public int? Id { get; set; }
        public int? PmId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public bool? IsNdaSigned { get; set; }
        public int? StatusId { get; set; }
        public string Status { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}