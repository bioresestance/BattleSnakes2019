using System;
using System.IO;
using System.Net;

namespace BattleSnake2019
{
    public class BattleSnake
    {

        public BattleSnake()
        { 
        }

        // Handler for incoming HTTP Requests.
        public string HTTPRequestHandler(HttpListenerRequest request)
        {
            string responseString = $"<HTML><BODY>You should not be seeing this, Something went wrong</BODY></HTML>";
            
            // Verify the request is a POST method
            if (request.HttpMethod == "POST")
            {
                // Get the data from the HTTP stream
                var body = new StreamReader(request.InputStream).ReadToEnd();

                // Ensure the URL was to us.
                // TODO: This might change if not on local machine, so it might need to be changed.
                if (request.Url.ToString().Contains("http://localhost:8080/"))
                {
                    // Remove the Front part of the URL to determine the endpoint being requested.
                    string endPoint = request.Url.ToString().Remove(0, "http://localhost:8080/".Length );  
                    
                    
                    switch (endPoint)
                    {
                        case "start":
                            responseString = "{ \"color\": \"#ff00ff\" }";
                            break;                   
                        case "move":
                            responseString = "{ \"move\": \"right\" }";
                            break;              
                        case "end":
                            // End payload is a don't care, so put nothing in.
                            responseString = "";
                            break;          
                        case "ping" :
                            // Ping doesn't have a payload.
                            responseString = "";
                            break;
                        default:
                            responseString = $"<HTML><BODY>My web page.<br>{DateTime.Now}</BODY></HTML>";
                            break;
                    }
                }  
            }
             
            return responseString;
        }
        
        
       // private string handleStartEndpoint( )
        
        
        
    }
}