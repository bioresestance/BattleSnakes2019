using System;
using System.IO;
using System.Net;

namespace BattleSnake2019
{
    public class BattleSnake
    {
        // Defines the possible Endpoints that requests can go to.
        protected enum EndPoints
        {
            Start = 0,
            Move = 1,
            End = 2,
            Ping = 3
        }

        protected virtual EndPoints EndPointStringToEnum(string endPoint)
        {
            switch (endPoint)
            {
                case "start":
                    return EndPoints.Start;
                case "move":
                    return EndPoints.Move;
                case "end":
                    return EndPoints.End;
                case "ping":
                    return EndPoints.Ping;
                default:
                    return EndPoints.Ping;
            } 
        }
        
        
        // JSON parser used to parse incoming json objects into something we can use.
        private readonly RequestJsonParser _parser;
        
        // Holds all of the current board information, such as location of food and snakes.
        private Board _currentBoard;

        // Holds the current URL that the snake is running on.
        private readonly string _systemUrl;
        
        public BattleSnake(string systemUrl)
        {
            _systemUrl = systemUrl;
            _currentBoard = new Board();
            _parser = new RequestJsonParser();
        }

        // Handler for incoming HTTP Requests.
        public string HTTPRequestHandler(HttpListenerRequest request)
        {
            var responseString = "<HTML><BODY>You should not be seeing this, Something went wrong</BODY></HTML>";
            
            // Verify the request is a POST method
            if (request.HttpMethod != "POST") return responseString;
            
            
            // Get the data from the HTTP stream
            var body = new StreamReader(request.InputStream).ReadToEnd();

            // Ensure the URL was to us.
            // TODO: This might change if not on local machine, so it might need to be changed.
            if (!request.Url.ToString().Contains(_systemUrl)) return responseString;
            
            
            // Remove the Front part of the URL to determine the endpoint being requested.
            var endPoint = EndPointStringToEnum(request.Url.ToString().Remove(0, _systemUrl.Length ));

            // Pings just need to respond with anything, so nothing to parse.
            if (endPoint == EndPoints.Ping) return "Yup, Still Here!";
            
            // Now parse the JSON into an object we can work with.
            var snakeRequest = _parser.Parse(body);
                        
            if(snakeRequest == null) throw new ArgumentNullException(nameof(snakeRequest));
            
            // Finally handle the request and get a response to send back.
            responseString = HandleRequest(snakeRequest, endPoint);
            
            return responseString;
        }

        protected virtual string HandleRequest(BattleSnakeRequest request, EndPoints endPoint)
        {
            if (endPoint == EndPoints.Start)
                return "{ \"color\": \"#ff00ff\" }";
            else if (endPoint == EndPoints.Move)
                return "{ \"move\": \"right\" }";
            else if (endPoint == EndPoints.End)
                return "";
            else
                return $"<HTML><BODY>My web page.<br>{DateTime.Now}</BODY></HTML>";
        }
        
        
       // private string handleStartEndpoint( )
        
        
        
    }
}