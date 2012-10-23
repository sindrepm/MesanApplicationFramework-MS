using System.Threading.Tasks;
using MesanMobilityFramework.Common.Interfaces;
using Newtonsoft.Json;

namespace MesanMobilityFramework.Common.Communication
{
    public class DefaultJsonSerializer : IRestResponseConverter
    {
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<string> SerializeObjectAsync(object value)
        {
            return await JsonConvert.SerializeObjectAsync(value);
        }

        public async Task<T> DeserializeObjectAsync<T>(string value)
        {
            return await JsonConvert.DeserializeObjectAsync<T>(value);
        }
    }
}
