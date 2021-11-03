using System.Threading.Tasks;

namespace CatQL.GraphQL.Client
{
    public interface IGqlClient{
        Task<GqlRequestResponse> PostQueryAsync(); 
    }
}