﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MayaWebServer.Server.Http;

namespace MayaWebServer.Server.Responses
{
    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
            : base(HttpStatusCode.NotFound) { }
    }
}
