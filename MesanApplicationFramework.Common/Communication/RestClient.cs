using System;
using System.Net;
using System.Threading.Tasks;
using MesanApplicationFramework.Common.Interfaces;

namespace MesanApplicationFramework.Common.Communication
{
    public class RestClient
    {
        public string BaseAddress { get; private set; }
        public ICredentials Credentials { get; set; }
        public IRestResponseConverter ResponseConverter { get; set; }
        public TimeSpan ConnectionTimeout { get; set; }

        public RestClient()
        {
            ConnectionTimeout = TimeSpan.FromSeconds(15);
            ResponseConverter = new DefaultJsonRestResponseConverter();
        }

        public RestClient(string baseAddress) : this()
        {
            BaseAddress = baseAddress;
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>(RestCommand<T> command) where T : class,new()
        {
            var data = command.Data != null ? ResponseConverter.SerializeObject(command.Data) : string.Empty;

            var executionContext = new RestExecutionContext(this);

            var response = await executionContext.GetResponseAsync(command, data);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return new RestResponse<T>
            {
                Content = result,
                Data = await ResponseConverter.DeserializeObjectAsync<T>(result)
            };
        }

        public async Task<RestResponse> ExecuteAsync(RestCommand command)
        {
            var executionContext = new RestExecutionContext(this);
            var response = await executionContext.GetResponseAsync(command);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return new RestResponse { Content = result };
        }
    }
}