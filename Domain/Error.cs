using System;

namespace Domain
{
    public class Error
    {
        public Error()
        {
            DateTime = DateTime.Now;
        }
        public int Id { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public DateTime DateTime { get; set; }
        public int? StatusCode { get; set; }
        public string Data { get; set; }
        public string ErrorMessage { get; set; }
        public string ClientIp { get; set; }
    }
}
