using System.Threading.Tasks;

namespace CatQL.GraphQL.Client
{
    public interface IGqlClient{
     /// <summary>
     /// Creates and HTTP Post Request to an GraphQL Endpoint with an JSON payload containing
     /// the Query string and Query variables. 
     /// </summary>
     /// <param name="query"></param>
     /// <typeparam name="T"></typeparam>
     /// <returns>GqlRequstResponse object containing object T deserialized from the JSON response</returns>
        Task<GqlRequestResponse<T>> PostQueryAsync<T>(GqlQuery query); 
    }
}