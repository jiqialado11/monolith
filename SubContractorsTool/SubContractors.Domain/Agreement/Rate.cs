using System;
using SubContractors.Common.EfCore;

namespace SubContractors.Domain.Agreement
{
    public class Rate : Entity<int>
    {
        public Rate()
        {
            
        }

        public Rate(int id)
        {
            Id = id;
        }

        public void AssignStaff(SubContractor.Staff.Staff staff)
        {
            Staff = staff;
        }

        public void AssignRateUnit(RateUnit unit)
        {
            Unit = unit;
        }

        public void Update(string name, decimal rateValue, DateTime fromDate, DateTime toDate, string description)
        {
            Name = name;
            RateValue = rateValue;
            FromDate = fromDate;
            ToDate = toDate;
            Description = description;
        }
        public SubContractor.Staff.Staff Staff { get; set; }
        public string Name { get; set; }
        public decimal RateValue { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public RateUnit Unit { get; set; }
        public Addendum Addendum { get; set; }
    }
}