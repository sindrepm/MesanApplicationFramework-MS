using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using MesanMobilityFramework.Common.Interfaces;

namespace MesanMobilityFramework.Common.Communication
{
    public class DefaultXmlRestResponseConverter : IRestResponseConverter
    {
        public string SerializeObject(object value)
        {
            return value.ToString();
        }

        public T DeserializeObject<T>(string value) where T : class, new()
        {
            if (typeof(T) == typeof(XDocument))
                return XDocument.Parse(value) as T;

            throw new InvalidOperationException(string.Format("The type '{0}' is not valid for this converter.", typeof(T).FullName));
        }

        public Task<string> SerializeObjectAsync(object value)
        {
            return Task.Run(() => value.ToString());
        }

        public Task<T> DeserializeObjectAsync<T>(string value) where T : class, new()
        {
            if (typeof(T) == typeof(XDocument))
                return Task.Run(() => XDocument.Parse(value)) as Task<T>;

            throw new InvalidOperationException(string.Format("The type '{0}' is not valid for this converter.", typeof(T).FullName));
        }
    }
}