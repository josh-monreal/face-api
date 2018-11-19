using static FA.Business.Enumerations;

namespace FA.Business.Models
{
    public class Response
    {
        public object Data { get; set; }
        public Status Status { get; set; }
        public string Message { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }
}
