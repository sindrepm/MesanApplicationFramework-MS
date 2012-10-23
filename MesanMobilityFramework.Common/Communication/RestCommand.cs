using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesanMobilityFramework.Common.Communication
{
    public abstract class RestCommand
    {
        public Dictionary<string,string> Parameters { get; private set; }

        public abstract string ResourceUri { get; }
        public abstract HttpMethod HttpMethod { get; }

        protected RestCommand()
        {
            Parameters = new Dictionary<string, string>();
        }


        public void AddParameter(string name, string value)
        {
            Parameters.Add(name, value);
        }

        public virtual string JsonContent() { return string.Empty; }
    }

    public abstract class RestCommand<T> : RestCommand
    {
        public T Object { get; set; }
    }
}
