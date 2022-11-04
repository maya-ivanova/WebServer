using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MayaWebServer.Server.Common;
using MayaWebServer.Server.Http;

namespace MayaWebServer.Server.Responses
{
    public class ContentResponse : HttpResponse
    {
       public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);
            var textLength = Encoding.UTF8.GetByteCount(text).ToString();
            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", textLength);

            this.Content = text;
        }
    }
}
