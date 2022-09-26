using RestSharp;

namespace SubContractors.Common.RestSharp
{
    public interface IApplicationExternalQueryHandler
    {
        RestClient QueryClient { get; }

        void SetConfiguration(IRestSharpOptions clientOptions);
    }
}
