namespace API.Models
{
    public class Response
    {
        public Response(string message, bool status, object data)
        {
            Message = message;
            Status = status;
            Data = data;
        }

        public Response(string message, bool status)
        {
            Message = message;
            Status = status;
        }

        public string Message { get; set; }
        public bool Status { get; set; }
        public object Data { get; set; }
    }
}
