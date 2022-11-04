using MayaWebServer.Server.Http;
using System.Collections;
using System.Collections.Generic;

namespace MayaWebServer.Server.Http
{
    public class HttpHeaderCollection : IEnumerable<HttpHeader>
    {
        //we create owr own collection in order to limit the external access to our Headers, i.e. so that the browser or anyone could not be able to delete Headers

        private readonly Dictionary<string, HttpHeader> headers;
        public HttpHeaderCollection()
        => this.headers = new Dictionary<string, HttpHeader>();

        //we initialize the collection in the Constructor

        public int Count => this.headers.Count;
        public void Add(string name, string value)
        { 
        
            var header = new HttpHeader(name, value);
        
            this.headers.Add(name, header);
        }

        public IEnumerator<HttpHeader> GetEnumerator()
       => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
    }
}
