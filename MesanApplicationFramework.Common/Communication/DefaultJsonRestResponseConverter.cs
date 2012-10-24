using System.Threading.Tasks;
using MesanApplicationFramework.Common.Interfaces;
using Newtonsoft.Json;

namespace MesanApplicationFramework.Common.Communication
{
    public class DefaultJsonRestResponseConverter : IRestResponseConverter
    {
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T DeserializeObject<T>(string value) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<string> SerializeObjectAsync(object value)
        {
            return await JsonConvert.SerializeObjectAsync(value);
        }

        public async Task<T> DeserializeObjectAsync<T>(string value) where T : class, new()
        {
            return await JsonConvert.DeserializeObjectAsync<T>(value);
        }
    }
}
