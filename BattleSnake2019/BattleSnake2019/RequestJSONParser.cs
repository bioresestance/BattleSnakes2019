using Newtonsoft.Json.Linq;

namespace BattleSnake2019
{
    public class RequestJSONParser
    {

        // Parses a JSON string and converts it to a BattleSnakeRequest object.
        public BattleSnakeRequest parse(string json)
        {
            BattleSnakeRequest request = new BattleSnakeRequest();
            
            JObject jObject = JObject.Parse(json);

            JToken game = jObject["game"];

            request.Game.Id = game["id"].ToString();

            return request;
        }       
        
    }
}