namespace MesanApplicationFramework.Common.Communication
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
