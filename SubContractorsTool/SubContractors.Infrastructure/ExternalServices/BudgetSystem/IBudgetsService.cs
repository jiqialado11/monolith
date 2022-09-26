using SubContractors.Infrastructure.ExternalServices.BudgetSystem.RequestModels;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels.BudgetDictionaries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.ExternalServices.BudgetSystem
{
    public interface IBudgetsService
    {
        Task<RegisterInvoiceResponse> RegisterInvoicesAsync(List<RegisterInvoiceRequest> requests);

        Task<BudgetDictionariesResponse> GetBudgetDictionariesAsync();
    }
}
