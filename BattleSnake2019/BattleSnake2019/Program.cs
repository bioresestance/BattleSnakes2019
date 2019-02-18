using System;
using System.IO;
using System.Net;

namespace BattleSnake2019
{
    class Program
    {
        static void Main(string[] args)
        {

            var URL = "http://localhost:8080/";
            
            // Main object that handles all decisions.
            BattleSnake snake = new BattleSnake(URL);
            
            // List of all the endpoints for the BattleSnake.
            string[] endpoints = new string[]
            {
                URL + "start/",
                URL + "move/",
                URL + "end/",
                URL + "ping/"
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