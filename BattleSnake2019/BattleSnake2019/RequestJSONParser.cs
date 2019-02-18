using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BattleSnake2019
{
    public class RequestJsonParser
    {

        // Parses a JSON string and converts it to a BattleSnakeRequest object.
        public BattleSnakeRequest Parse(string json)
        {
            if (json == null) throw new ArgumentNullException(nameof(json));
            
            var request = new BattleSnakeRequest();
            try
            {
                var jObject = JObject.Parse(json);
                // If the json is empty, exit.
                if ( !jObject.HasValues ) throw new JsonException("Empty JSON");
               
                // Parse the Game object.
                var game = jObject["game"];
                request.Game.Id = (string) game["id"];
                
                // Parse the Turn object.
                request.Turn = (long) jObject["turn"];

                // Parse the Board object.
                var board = jObject["board"];
                request.Board = ParseBoard(board);

                var snake = jObject["you"];
                request.You = ParseSnake(snake);

            }
            catch
            {
                Console.WriteLine("Failed to parse JSON string.");
            }

            return request;
        }


        // Parses out a Board JSON token and returns a Board object.
        private Board ParseBoard(JToken board)
        {
            var parsedBoard = new Board {Height = (long) board["height"], Width = (long) board["width"]};

            // Now load in the location of all food items on the board.
            foreach (var nextFood in board["food"] )
            {
                 // Extract the x and y of each food item.
                var pos = new Position {X = (long) nextFood["x"], Y = (long) nextFood["y"]};

                // Add the food to the list.
                parsedBoard.Food.Add(pos);

                Console.WriteLine("Food at ({0},{1})", pos.X, pos.Y);
            }

            // Next parse out all of the snake objects from the JSON.
            foreach (var snake in board["snake"])
            {
                var nextSnake = ParseSnake(snake);
                parsedBoard.Snakes.Add(nextSnake);
            }
            
            return parsedBoard;
        }

        // Parses a JSON description of a snake object.
        private Snake ParseSnake(JToken snake)
        {

            var parsedSnake = new Snake
            {
                Id = (string) snake["id"], Health = (long) snake["health"], Name = (string) snake["name"]
            };

            // Parse out the simple items and put them in the object.

            // Parse out the position of each part of hte snakes body. 
            var pos = new Position();
            
            foreach (var body in snake["body"])
            {
                pos.X = ( long) body["x"];
                pos.Y = ( long) body["y"];
                
                parsedSnake.Body.Add(pos);
            }
            return parsedSnake;
        }
        
        
    }
}