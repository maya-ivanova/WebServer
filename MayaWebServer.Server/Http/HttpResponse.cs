using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;

namespace MayaWebServer.Server.Http
{
    public abstract class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode, HttpHeaderCollection headers, string content)
        {
            StatusCode = statusCode;
            Headers = headers;
            Content = content;
        }

        public HttpResponse(HttpStatusCode statusCode)
        { 
            this.StatusCode = statusCode;
            this.Headers.Add("Server", "Maya Web Server");
            this.Headers.Add("Date", $"{DateTime.UtcNow:r}");
        }

        public HttpStatusCode StatusCode { get; private set; }

        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();

        public string Content { get; init; }
    public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"HTTP / 1.1 {(int)this.StatusCode} {this.StatusCode}");

            foreach (var header in this.Headers)
            {
                result.AppendLine(header.ToString());
            }

            if (!string.IsNullOrEmpty(this.Content))
            {
                result.AppendLine();
                result.Append(this.Content);
            }
            return result.ToString();
        }
    }
}
