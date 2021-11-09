using CatQL.GraphQL.QueryResponses;
using Newtonsoft.Json;

namespace CatQLClient.QueryResponses
{
    /// <summary>
    /// Object respresentation of the Error object received if and error is generated when sending an Query. 
    /// Class is subtype of GqlRequestResponse and must be checked for when using the client, because errors
    /// are implicit, i.e no Explicit errors are thrown when an error is generated. 
    /// </summary>
    /// <typeparam name="T">The type of Data that was queried for</typeparam>
    public class GqlRequestErrorResponse<T> : GqlRequestResponse<T>
    {
        [JsonProperty("errors")]
        public object[] Errors { get; set; }
        
    }
}