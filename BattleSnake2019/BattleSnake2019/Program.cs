using System;
using System.IO;
using System.Net;

namespace BattleSnake2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Webserver ws = new Webserver( "http://localhost:8080/", SendResponse);
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }
 
        public static string SendResponse(HttpListenerRequest request)
        {
            Console.WriteLine("Request type: {0}", request.HttpMethod );

            //Webserver.DisplayWebHeaderCollection(request);
            
            // Get the data from the HTTP stream
            var body = new StreamReader(request.InputStream).ReadToEnd();
            
            Console.WriteLine("Body:  {0}", body);
            
            
            return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);    
        }
    }
}