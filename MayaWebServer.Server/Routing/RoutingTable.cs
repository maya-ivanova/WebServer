using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MayaWebServer.Server.Common;
using MayaWebServer.Server.Http;
using MayaWebServer.Server.Responses;

namespace MayaWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<Http.HttpMethod, Dictionary<string, HttpResponse>> routes;

        public RoutingTable() => this.routes = new()
        {
            [Http.HttpMethod.Get] = new(),
            [Http.HttpMethod.Post] = new(),
            [Http.HttpMethod.Put] = new(),
            [Http.HttpMethod.Delete] = new(),
        };

        public IRoutingTable Map(Http.HttpMethod method, string path, HttpResponse response)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(response, nameof(response));

            this.routes[method][path] = response;
            return this;
        }


        public IRoutingTable MapGet(string path, HttpResponse response)
=> Map(Http.HttpMethod.Get, path, response);

        //    method switch
        //{
        //    Http.HttpMethod.Get => this.MapGet(path, response),
        //    _ => throw new InvalidOperationException($"Method '{method}' is not supported.")
        //    //with this code I enable a chain --> .MapGet().MapGet().etc...
        //};

        public IRoutingTable MapPost(string path, HttpResponse response)
=> Map(Http.HttpMethod.Post, path, response);


        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            if (!this.routes.ContainsKey(requestMethod) || !this.routes[requestMethod].ContainsKey(requestPath))
            {
                return new NotFoundResponse();
            }

            return this.routes[requestMethod][requestPath];
        }
    }
}
