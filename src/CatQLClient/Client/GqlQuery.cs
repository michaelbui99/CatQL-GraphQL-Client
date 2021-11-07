using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CatQL.GraphQL.Client
{
    public class GqlQuery
    {
        [Required]
        [JsonProperty("query")]
        public string Query { get; set; }
        [JsonProperty("operationName", NullValueHandling =NullValueHandling.Ignore)]
        public string OperationName { get; set; }
        [JsonProperty("variables", NullValueHandling =NullValueHandling.Ignore)]
        public string Variables { get; set; }
    }
}