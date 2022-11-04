using MayaWebServer.Server.Http;
using MayaWebServer.Server.Routing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MayaWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;
        private readonly RoutingTable routingTable;

        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfiguration)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            listener = new TcpListener(this.ipAddress, port);
           
            routingTableConfiguration(this.routingTable = new RoutingTable());

            //what we want here is to call the configuration we've set up in the StartUp; we take the private routingTable from here, we fetch it to the one we have set up in the StartUp and now we can start using it
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable) 
            : this ("127.0.0.1", port, routingTable)
        { }

        public HttpServer(Action<IRoutingTable> routingTable) 
            : this(5050, routingTable) { }

        public async Task Start()
        {

            this.listener.Start();

            Console.WriteLine($"Server started on port {port}");
            Console.WriteLine("Listening for requests ...");

            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();
                var requestText = await this.ReadRequest(networkStream);

               // Console.WriteLine(requestText);

                var request = HttpRequest.Parse(requestText);

                var response = this.routingTable.MatchRequest(request);

                Console.WriteLine(response.ToString());

                //await WriteResponse(networkStream);
                await WriteResponse(networkStream, response);
                connection.Close();
            }

        }

        private async Task WriteResponse(NetworkStream networkStream, HttpResponse response)
        {
        //    var content = "Hello from my server!! \r\n Здрасти :) آني مبسوطة بمعرفتك !!!! مرحبا كيفك؟";

        //    var contentLength = Encoding.UTF8.GetByteCount(content);


        //    var response = $@"
        //HTTP/1.1 200 OK
        //Server: MyWebServer
        //Date: {DateTime.UtcNow:R}
        //Content-Length: {contentLength}
        //Content-Type: text/plain; charset=UTF-8

        //{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responseBytes);


        }

        //private async Task WriteResponse(
        //   NetworkStream networkStream,
        //   HttpResponse response)
        //{
        //    var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

        //    await networkStream.WriteAsync(responseBytes);


        //    if (response.HasContent)
        //    {
        //        await networkStream.WriteAsync(response.Content);
        //    }
        //}

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var totalBytes = 0;

            var requestBuilder = new StringBuilder();

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);

                totalBytes += bytesRead;

                if (totalBytes > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large.");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }


    }
}