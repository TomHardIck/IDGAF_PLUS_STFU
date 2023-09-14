namespace API
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T data)
        {
            Suceeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        public T Data { get; set; }
        public bool Suceeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
