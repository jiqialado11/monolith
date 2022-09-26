using System.Collections.Generic;

namespace SubContractors.Common.Redis
{
    public interface IInvalidateCacheRequest
    {
        List<string> GetDomainsIdentifiers();
    }
}
