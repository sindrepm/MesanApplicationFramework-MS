using System.Threading.Tasks;

namespace MesanApplicationFramework.Common.Interfaces
{
    
    public interface IRestResponseConverter
    {
        string SerializeObject(object value);
        T DeserializeObject<T>(string value) where T : class,new();
        Task<string> SerializeObjectAsync(object value);
        Task<T> DeserializeObjectAsync<T>(string value) where T : class,new();
    }
}
