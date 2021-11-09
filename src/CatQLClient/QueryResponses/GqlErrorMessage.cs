using Newtonsoft.Json;

namespace CatQLClient.QueryResponses
{
    public class GqlErrorMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        
        
    }
}