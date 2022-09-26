using SubContractors.Common.RestSharp;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem
{
    public class BudgetsSystemOptions : IBudgetsOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
        public string Domain { get; set; }
        public string AuthenticationType { get; set; }
        public string RegisterInvoicePath { get; set; }
        public string CurrencyAndPaymentMethodsPath { get; set; }
    }

    public interface IBudgetsOptions : IRestSharpOptions
    {
        public string RegisterInvoicePath { get; set; }
        public string CurrencyAndPaymentMethodsPath { get; set; }
    }
}
