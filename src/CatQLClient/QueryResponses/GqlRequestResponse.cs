using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CatQL.GraphQL.QueryResponses
{
    /// <summary>
    /// Object representation of the Query resonse. The object contains the data that was queried. 
    /// </summary>
    /// <typeparam name="T">Type of Data that is being queried</typeparam>
    public class GqlRequestResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
        
    }
}