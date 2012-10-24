using System.Collections.Generic;
using System.Linq;

namespace MesanApplicationFramework.Common.Communication
{
    public class RestCommand
    {
        public Dictionary<string, string> Parameters { get; private set; }

        public virtual string ResourceUri { get; private set; }
        public virtual HttpMethod HttpMethod { get; private set; }
        public string FullResourceUri { get { return ResourceUri + GetParametersAsQueryString(); } }

        public RestCommand()
        {
            Parameters = new Dictionary<string, string>();
            ResourceUri = string.Empty;
            HttpMethod = HttpMethod.Get;
        }

        public RestCommand(string resourceUri, HttpMethod method)
        {
            Parameters = new Dictionary<string, string>();
            ResourceUri = resourceUri;
            HttpMethod = method;
        }

        public void AddParameter(string name, string value)
        {
            Parameters.Add(name, value);
        }

        public string GetParametersAsQueryString()
        {
            if (!Parameters.Any())
                return string.Empty;

            string separator = ResourceUri.Contains("?") ? "&" : "?";

            return separator + string.Join("&",
                from kvp in Parameters
                select string.Format("{0}={1}", kvp.Key, kvp.Value));
        }

        public override string ToString()
        {
            return FullResourceUri;
        }
    }

    public class RestCommand<T> : RestCommand
    {
        public RestCommand() { }

        public RestCommand(string resourceUri, HttpMethod method) : base(resourceUri, method) { }

        public RestCommand(string resourceUri, HttpMethod method, T data)
            : base(resourceUri, method)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}