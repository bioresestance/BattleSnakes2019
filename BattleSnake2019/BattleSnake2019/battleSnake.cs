using System;
using System.Drawing;
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
   
        protected EndPoints EndPointStringToEnum(string endPoint)
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

        // Holds the current URL that the snake server is running on.
        private readonly string _systemUrl;

        private readonly Color _snakeColor;

        private Snake _ourSnake;

        private SnakeLogicEngine _snakeBrain;
        
        public BattleSnake(string systemUrl, Color snakeColor)
        {
            this._systemUrl = systemUrl;
            this._snakeColor = snakeColor;
            this._currentBoard = new Board();
            this._parser = new RequestJsonParser();
            this._ourSnake = new Snake();
            this._snakeBrain = new SnakeLogicEngine();
        }

        // Handler for incoming HTTP Requests.
        public string HTTPRequestHandler(HttpListenerRequest request)
        {
            var responseString = "You should not be seeing this, Something went wrong";
            
            // Verify the request is a POST method
            if (request.HttpMethod != "POST") return responseString;
            
            
            // Get the data from the HTTP stream
            var body = new StreamReader(request.InputStream).ReadToEnd();

            // Ensure the URL was to us.
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

        // Runs the handler for the given endpoint and request from the game engine.
        protected string HandleRequest(BattleSnakeRequest request, EndPoints endPoint)
        {
            if (endPoint == EndPoints.Start)
                return HandleStartRequest(request);
            else if (endPoint == EndPoints.Move)
                return HandleMoveRequest(request);
            else if (endPoint == EndPoints.End)
                return "";
            else
                return $"<HTML><BODY>My web page.<br>{DateTime.Now}</BODY></HTML>";
        }


        // Handles a Start request. Saves current board and sends desired color.
        private string HandleStartRequest(BattleSnakeRequest startRequest)
        {
            // Save the starting board so we know where everything is.
            _currentBoard = startRequest.Board;

            // Turn the color into a hex string.
            var color = $"#{_snakeColor.R:X}{_snakeColor.G:X}{_snakeColor.B:X}";

            // Th response to this request is the color we want to be.
            return "{ \"color\":" + "\"" + color + "\"" + " }";
        }


        private string HandleMoveRequest(BattleSnakeRequest moveRequest)
        {
            
            // Save the current board and our snake so we know where everything is.
            _currentBoard = moveRequest.Board;
            _ourSnake = moveRequest.You;

            var direction = _snakeBrain.decideMoveDirection( _currentBoard, _ourSnake );
                    
            return "{ \"move\":" + "\"" + direction + "\"" + " }";
        }
        
        
        
    }
}