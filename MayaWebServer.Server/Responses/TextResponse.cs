using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MayaWebServer.Server.Common;
using MayaWebServer.Server.Http;

namespace MayaWebServer.Server.Responses
{
    public class TextResponse : ContentResponse
    {
       
        public TextResponse(string text) 
            : base(text, "text/plain; charset=UTF-8")
        {
         }

    }
}
