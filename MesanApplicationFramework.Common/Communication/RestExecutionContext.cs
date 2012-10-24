using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MesanMobilityFramework.Common.Communication
{
    public class RestExecutionContext
    {
        private readonly string _baseAddress;
        private readonly ICredentials _credentials;
        private readonly TimeSpan _timeout;

        public RestExecutionContext(RestClient restClient)
        {
            _baseAddress = restClient.BaseAddress;
            _credentials = restClient.Credentials;
            _timeout = restClient.ConnectionTimeout;
        }

        public async Task<HttpResponseMessage> GetResponseAsync(RestCommand command, string data = "")
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    if (_credentials != null)
                        handler.Credentials = _credentials;

                    using (var httpClient = new HttpClient(handler))
                    {
                        ConfigureHttpClient(httpClient);

                        switch (command.HttpMethod)
                        {
                            case HttpMethod.Get:
                                return await httpClient.GetAsync(command.FullResourceUri);
                            case HttpMethod.Put:
                                return await httpClient.PutAsync(command.FullResourceUri, new StringContent(data));
                            case HttpMethod.Post:
                                return await httpClient.PostAsync(command.FullResourceUri, new StringContent(data));
                            case HttpMethod.Delete:
                                return await httpClient.DeleteAsync(command.FullResourceUri);
                        }

                        throw new Exception(string.Format("The command '{0}' does not have a valid HttpMethod. (Type: {1})", command, command.GetType().FullName));
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("An error occured while performing the request.", e);
            }
        }

        private void ConfigureHttpClient(HttpClient httpClient)
        {
            if (!string.IsNullOrEmpty(_baseAddress))
                httpClient.BaseAddress = new Uri(_baseAddress);

            httpClient.Timeout = _timeout;
        }
    }
}