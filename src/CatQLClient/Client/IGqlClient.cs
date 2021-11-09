using System.Threading.Tasks;
using CatQL.GraphQL.QueryResponses; 

namespace CatQL.GraphQL.Client
{
    public interface IGqlClient{
     /// <summary>
     /// Creates and HTTP Post Request to an GraphQL Endpoint with an JSON payload containing
     /// the Query string and Query variables. 
     /// </summary>
     /// <param name="query">The GraphQL query</param>
     /// <typeparam name="T">
     /// ResponseType class that must contain a property of the samet type that the Query returns
     /// and the Property name must be the same as the type. 
     /// </typeparam>
     /// <returns>
     /// GqlRequstResponse object containing ResponseType deserialized from the JSON response.
     /// </returns>
        Task<GqlRequestResponse<T>> PostQueryAsync<T>(GqlQuery query); 
    }
}