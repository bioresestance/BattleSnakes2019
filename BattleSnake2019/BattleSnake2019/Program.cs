using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace BattleSnake2019
{
    class Program
    {
        static void Main(string[] args)
        {
            // URL of the server.
            var url = "http://localhost:8080/";
            
            var snakeColor = Color.Aquamarine;
            
            // Main object that handles all decisions.
            BattleSnake snake = new BattleSnake(url, snakeColor);
            
            // List of all the endpoints for the BattleSnake.
            string[] endpoints = new string[]
            {
                url + "start/",
                url + "move/",
                url + "end/",
                url + "ping/"
            };
        
            // Create a webserver, listening to the above endpoints. All responses sent to the snake handler.
            Webserver ws = new Webserver( endpoints, snake.HTTPRequestHandler);
            
            // Start up the server. Creates a separate thread for each request.
            ws.Run();
            
            Console.WriteLine("Battle Snake HTTP Webserver is set up. Press any key to exit!");
            Console.ReadKey();
            ws.Stop();
        }
    }
}