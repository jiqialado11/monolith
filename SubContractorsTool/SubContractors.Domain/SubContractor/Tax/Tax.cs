using SubContractors.Common.EfCore;
using System;

namespace SubContractors.Domain.SubContractor.Tax
{
    public class Tax : Entity<int>
    {
        public Tax()
        {
            
        }

        public Tax(int id)
        {
            Id = id;
        }

        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public TaxType TaxType { get; set; }
        public SubContractor SubContractor { get; set; }

        public void Create(string name, string taxNumber, string link, DateTime date, TaxType taxType, SubContractor subContractor)
        {
            Name = name;
            TaxNumber = taxNumber;
            Link = link;
            Date = date;
            SubContractor = subContractor;
            TaxType = taxType;
        }

        public void Update(string name, string taxNumber, string link, DateTime date, TaxType taxType)
        {
            Name = name;
            TaxNumber = taxNumber;
            Link = link;
            Date = date;
            TaxType = taxType;
        }
    }
}