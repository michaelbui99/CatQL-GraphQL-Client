using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CatQL.GraphQL.Client
{
    public class GqlRequestResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
        
    }
}