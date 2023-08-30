using System;
using Common.Enum;

namespace Domain
{
    public class Information
    {
        public Information()
        {
            DateTime = DateTime.Now;
        }
        public int Id { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public ContextType Type { get; set; }
        public DateTime DateTime { get; set; }
        public int? StatusCode { get; set; }
        public string Data { get; set; }
        public string ClientIp { get; set; }
    }
}
