using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CatQL.GraphQL.Client
{
    public class GqlRequestResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
        
    }
}