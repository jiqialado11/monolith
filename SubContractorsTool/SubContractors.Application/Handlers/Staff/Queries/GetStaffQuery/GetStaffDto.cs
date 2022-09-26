using System;

namespace SubContractors.Application.Handlers.Staff.Queries.GetStaffQuery
{
    public class GetStaffDto
    {
        public int? Id { get; set; }
        public int? PmId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Position { get; set; }
        public DateTime? CannotLoginBefore { get; set; }
        public DateTime? CannotLoginAfter { get; set; }
        public string Qualification { get; set; }
        public string RealLocation { get; set; }
        public string CellPhone { get; set; }
        public bool? IsNdaSigned { get; set; }
        public string DepartmentName { get; set; }
        public string DomainLogin { get; set; }
    }
}
