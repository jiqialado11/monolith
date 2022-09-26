using RestSharp;
using System;
using System.Net;

namespace SubContractors.Common.RestSharp
{
    public class ApplicationQueryHandler : IApplicationExternalQueryHandler, IDisposable
    {
        private RestClient restClient;        

        public ApplicationQueryHandler()
        { }

        public RestClient QueryClient => restClient;

        public ApplicationQueryHandler(IRestSharpOptions settings)
        {
            var credential = new NetworkCredential(settings.UserName, settings.Password, settings.Domain);
            var credentialCache = new CredentialCache();
            credentialCache.Add(new Uri(settings.BaseUrl), settings.AuthenticationType, credential);
            var options = new RestClientOptions(settings.BaseUrl);
            options.Credentials = credentialCache;
            restClient = new RestClient(options);
        }

        public void SetConfiguration(IRestSharpOptions clientOptions)
        {
            var credential = new NetworkCredential(clientOptions.UserName, clientOptions.Password, clientOptions.Domain);
            var credentialCache = new CredentialCache();
            credentialCache.Add(new Uri(clientOptions.BaseUrl), clientOptions.AuthenticationType, credential);
            var options = new RestClientOptions(clientOptions.BaseUrl);
            options.Credentials = credentialCache;
            restClient = new RestClient(options);
        }


        public void Dispose()
        {
            restClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
