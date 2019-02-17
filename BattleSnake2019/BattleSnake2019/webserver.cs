using System;
using System.Net;
using System.Threading;
using System.Text;
 
namespace BattleSnake2019
{
   
    // Web serer code based on code found at https://codehosting.net/blog/BlogEngine/post/Simple-C-Web-Server.
    
    public class Webserver
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;
 
        public Webserver(string[] addresses, Func<HttpListenerRequest, string> method)
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException("HTTP Listener is not supported on this computer.");
                    
 
            // URI prefixes are required, for example 
            // "http://localhost:8080/index/".
            if (addresses == null || addresses.Length == 0)
                throw new ArgumentException("prefixes");
 
            // A responder method is required
            if (method == null)
                throw new ArgumentException("method");

            foreach (var address in addresses)
            {
                _listener.Prefixes.Add(address);
            }
            
 
            _responderMethod = method;
            _listener.Start();
        }
        
        public void Run()
        {
            // Creates a thread for each request.
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Console.WriteLine("Webserver running...");
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                string rstr = _responderMethod(ctx.Request);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch { } // suppress any exceptions
                            finally
                            {
                                // always close the stream
                                ctx.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch { } // suppress any exceptions
            });
        }
 
        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
        
        // Displays the header information that accompanied a request.
        public static void DisplayWebHeaderCollection(HttpListenerRequest request)
        {
            System.Collections.Specialized.NameValueCollection headers = request.Headers;
            // Get each header and display each value.
            foreach (string key in headers.AllKeys)
            {
                string[] values = headers.GetValues(key);
                if(values.Length > 0) 
                {
                    Console.WriteLine("The values of the {0} header are: ", key);
                    foreach (string value in values) 
                    {
                        Console.WriteLine("   {0}", value);
                    }
                }
                else
                    Console.WriteLine("There is no value associated with the header.");
            }
        }
    }
}