using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MesanMobilityFramework.Common.Interfaces;
using Newtonsoft.Json;

namespace MesanMobilityFramework.Common.Communication
{
    public class RestClient
    {
        private IRestResponseConverter _converter;

        public string BaseAddress { get; private set; }
        public ICredentials Credentials { get; set; }
        public IRestResponseConverter Converter
        {
            get
            {
                if (_converter == null)
                    _converter = new DefaultJsonSerializer();

                return _converter;
            }
            set { _converter = value; }
        }

        public RestClient()
        {
        }

        public RestClient(string baseAddress)
        {
            BaseAddress = baseAddress;
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>(RestCommand<T> command)
        {
            var jsonPayload = JsonConvert.SerializeObject(command.Object);

            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = Credentials;
                using (var httpClient = new HttpClient(handler))
                {
                    ConfigureHttpClient(httpClient);

                    HttpResponseMessage response;
                    switch (command.HttpMethod)
                    {
                        case HttpMethod.Get:
                            response = await httpClient.GetAsync(command.ResourceUri);
                            return await DeserializeResponseAsync<T>(await GetResponseAsStringAsync(response));

                        case HttpMethod.Put:
                            response = await httpClient.PutAsync(command.ResourceUri, new StringContent(jsonPayload));
                            return await DeserializeResponseAsync<T>(await GetResponseAsStringAsync(response));

                        case HttpMethod.Post:
                            response = await httpClient.PostAsync(command.ResourceUri, new StringContent(jsonPayload));
                            return await DeserializeResponseAsync<T>(await GetResponseAsStringAsync(response));
                        case HttpMethod.Delete:
                            response = await httpClient.DeleteAsync(command.ResourceUri);
                            return await DeserializeResponseAsync<T>(await GetResponseAsStringAsync(response));
                    }

                    throw new Exception(string.Format("Rest command '{0}' is not valid for an executable task. (Type: {1})", command, command.GetType().FullName));
                }
            }
        }

        public async Task<RestResponse> ExecuteAsync(RestCommand command)
        {
            using (var handler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(handler))
                {
                    ConfigureHttpClient(httpClient);

                    switch (command.HttpMethod)
                    {
                        case HttpMethod.Delete:
                            var response = await httpClient.DeleteAsync(command.ResourceUri);
                            return new RestResponse { Content = await GetResponseAsStringAsync(response) };
                    }

                    throw new Exception(string.Format("Rest command '{0}' is not valid for an executable task. (Type: {1})", command, command.GetType().FullName));
                }
            }
        }

        private void ConfigureHttpClient(HttpClient httpClient)
        {
            if (!string.IsNullOrEmpty(BaseAddress))
                httpClient.BaseAddress = new Uri(BaseAddress);

            httpClient.Timeout = TimeSpan.FromMilliseconds(3000);
        }

        private static async Task<string> GetResponseAsStringAsync(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private static async Task<RestResponse<T>> DeserializeResponseAsync<T>(string content)
        {
            return new RestResponse<T>
            {
                Content = content,
                Data = await JsonConvert.DeserializeObjectAsync<T>(content)
            };
        }
    }
}