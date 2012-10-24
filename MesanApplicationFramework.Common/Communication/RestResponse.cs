using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesanMobilityFramework.Common.Communication
{
    public class RestResponse
    {
        public string Content { get; set; }
    }

    public class RestResponse<T> : RestResponse
    {
        public T Data { get; set; }
    }
}
