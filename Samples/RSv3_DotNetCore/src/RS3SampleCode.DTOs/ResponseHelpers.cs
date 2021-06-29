using System.Net.Http;

namespace RS3SampleCode.DTOs
{
    public class ResponseHelpers
    {
        public HttpResponseMessage Resp { get; set; }

        public string RespContent { get; set; }

        public RSError RSErrors { get; set; }

        public bool Success { get; set; }
    }
}
