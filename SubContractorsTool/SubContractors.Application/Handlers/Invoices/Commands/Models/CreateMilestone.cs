using System;

namespace SubContractors.Application.Handlers.Invoices.Commands.Models
{
    public class CreateMilestone
    {
        public int PmId { get; set; }
        public double Amount { get; set; }
        public DateTime ToDate { get; set; }
        public string Name { get; set; }
    }
}
