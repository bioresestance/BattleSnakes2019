using System;
using System.IO;
using System.Net;

namespace BattleSnake2019
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main object that handles all decisions.
            BattleSnake snake = new BattleSnake();
            
            // List of all the endpoints for the BattleSnake.
            string[] endpoints = new string[]
            {
                "http://localhost:8080/start/",
                "http://localhost:8080/move/",
                "http://localhost:8080/end/",
                "http://localhost:8080/ping/"
            };
        
            // Create a webserver, listening to the above endpoints. All responses sent to snake handler.
            Webserver ws = new Webserver( endpoints, snake.HTTPRequestHandler);
            
            // Start up the server.
            ws.Run();
            
            Console.WriteLine("Battle Snake HTTP Webserver is set up. Press any key to exit!");
            Console.ReadKey();
            ws.Stop();
        }
    }
}