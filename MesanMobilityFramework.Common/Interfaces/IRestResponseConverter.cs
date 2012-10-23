using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesanMobilityFramework.Common.Interfaces
{
    public interface IRestResponseConverter
    {
        string SerializeObject(object value);
        T DeserializeObject<T>(string value);
        Task<string> SerializeObjectAsync(object value);
        Task<T> DeserializeObjectAsync<T>(string value);
    }
}
