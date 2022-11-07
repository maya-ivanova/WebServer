using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using MayaWebServer.Server;
using MayaWebServer.Server.Responses;

namespace MayaWebServer
{
    public class StartUp
    {
        public static async Task Main()
            => await new HttpServer(9090, routes => routes
            .MapGet("/", new TextResponse("Hello from 2022-11-02 meeeee!"))
            .MapGet("/Cats", new HtmlResponse("<h2>Hello from the CATS! القطط ترحبك</h2>"))
            .MapGet("/Dogs", new HtmlResponse("<h1>Hello from the dogs :) أهلا وسهلا من الكلاب </h1>")))
            .Start();
       
        //{
        //   var server = new HttpServer("127.0.0.1", 9090);
           // await server.Start();
        //}
    }
}