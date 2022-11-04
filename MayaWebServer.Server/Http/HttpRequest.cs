using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MayaWebServer.Server.Http
{
    public class HttpRequest
    {
        
            //private static Dictionary<string, HttpSession> Sessions = new();

            private const string NewLine = "\r\n";

            public HttpMethod Method { get; private set; }

            public string Path { get; private set; }

        public Dictionary<string, string> Query { get; private set; }
           // public QueryCollection Query { get; private set; }

            public HttpHeaderCollection Headers { get; private set; }

           // public CookieCollection Cookies { get; private set; }

        //    public FormCollection Form { get; private set; }

            public string Body { get; private set; }

            //public HttpSession Session { get; private set; }

           // public ServiceCollection Services { get; private set; }

            public static HttpRequest Parse(string request)
            {
                var lines = request.Split(NewLine);

                var startLine = lines.First().Split(" ");

                var method = ParseHttpMethod(startLine[0]);
                var url = startLine[1];

            var (path, query) = ParseUrl(url);
            //this is a 'tuple' variable - to define a tuple type, you specify types of all its data members and, optionally, the field names. 

            var headers = ParseHttpHeaders(lines.Skip(1));

            //var cookies = ParseCookies(headers);

            //var session = GetSession(cookies);

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join(NewLine, bodyLines);

                //var form = ParseForm(headers, body);

                return new HttpRequest
                {
                    Method = method,
                    Path = path,
                    Query = query,
                    Headers = headers,
                    Body = body

                };
            }

        private static HttpMethod ParseHttpMethod(string method)
        {
            //method = method.ToUpper() switch
            //{
            //    "GET" => Http.HttpMethod.Get.ToString(),
            //    "POST" => Http.HttpMethod.Post.ToString(),
            //    "PUT" => Http.HttpMethod.Put.ToString(),
            //    "DELETE" => Http.HttpMethod.Delete.ToString(),
            //    _ => throw new InvalidOperationException($"Method '{method}' is not supported.")
            //};


            try
            {
                return (HttpMethod)Enum.Parse(typeof(HttpMethod), method, true);
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Method '{method}' is not supported");
            }
        }

        // url = /Cats?name=Ivan&Age=5
        private static (string, Dictionary<string,string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?', 2);

            var path = urlParts[0];

            var query = new Dictionary<string, string>();

            //var query = urlParts.Length > 1
            //    ? ParseQuery(urlParts[1])
            //    : new QueryCollection();

            return (path, query);
        //tuples are used instead of initiating a Class and assigning 2 properties for it

        }

        //private static Dictionary<string, string> ParseQuery(string queryString)
        //{
        //    var query = new Dictionary<string, string>();



        //    var queryCollection = new QueryCollection();

        //    var parsedResult = ParseQueryString(queryString);

        //    foreach (var (name, value) in parsedResult)
        //    {
        //        queryCollection.Add(name, value);
        //    }

        //    return queryCollection;
        //}

        private static HttpHeaderCollection ParseHttpHeaders(IEnumerable<string> headerLines)
            {
                var headerCollection = new HttpHeaderCollection();

                foreach (var headerLine in headerLines)
                {
                    if (headerLine == string.Empty)
                    {
                        break;
                    }

                    var headerParts = headerLine.Split(":", 2);

                    if (headerParts.Length != 2)
                    {
                        throw new InvalidOperationException("Request is not valid.");
                    }

               var headerName = headerParts[0];
                var headerValue = headerParts[1].Trim();

                var header = new HttpHeader(headerName, headerValue);
                    
               
                headerCollection.Add(headerName, headerValue);

                //headerCollection.Add(headerName, headerValue);
                }

                return headerCollection;
            }

            //private static CookieCollection ParseCookies(HeaderCollection headers)
            //{
            //    var cookieCollection = new CookieCollection();

            //    if (headers.Contains(HttpHeader.Cookie))
            //    {
            //        var cookieHeader = headers[HttpHeader.Cookie];

            //        var allCookies = cookieHeader.Split(';');

            //        foreach (var cookieText in allCookies)
            //        {
            //            var cookieParts = cookieText.Split('=');

            //            var cookieName = cookieParts[0].Trim();
            //            var cookieValue = cookieParts[1].Trim();

            //            cookieCollection.Add(cookieName, cookieValue);
            //        }
            //    }
            //    return cookieCollection;
            //}

            //private static HttpSession GetSession(CookieCollection cookies)
            //{
            //    var sessionId = cookies.Contains(HttpSession.SessionCookieName)
            //        ? cookies[HttpSession.SessionCookieName]
            //        : Guid.NewGuid().ToString();

            //    if (!Sessions.ContainsKey(sessionId))
            //    {
            //        Sessions[sessionId] = new HttpSession(sessionId)
            //        {
            //            IsNew = true
            //        };
            //    }

            //    return Sessions[sessionId];
            //}

            //private static FormCollection ParseForm(HeaderCollection headers, string body)
            //{
            //    var formCollection = new FormCollection();
            //    if (headers.Contains(HttpHeader.ContentType)
            //        && headers[HttpHeader.ContentType] == HttpContentType.FormUrlEncoded)
            //    {
            //        var parsedResult = ParseQueryString(body);

            //        foreach (var (name, value) in parsedResult)
            //        {
            //            formCollection.Add(name, value);
            //        }
            //    }

            //    return formCollection;
            //}

            //private static Dictionary<string, string> ParseQueryString(string queryString)
            //    => HttpUtility.UrlDecode(queryString)
            //        .Split('&')
            //        .Select(part => part.Split('='))
            //        .Where(part => part.Length == 2)
            //        .ToDictionary(
            //            part => part[0],
            //            part => part[1],
            //            StringComparer.InvariantCultureIgnoreCase);
        }
    }
