using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CatQL.GraphQL.Helpers;

namespace CatQL.GraphQL.Client
{
    public class GqlClient : IGqlClient
    {
        /// <summary>
        /// GraphQL URL endpoint used for the HTTP request
        /// </summary>
        private readonly string _url;
        /// <summary>
        /// Internal instance of <see cref="HttpClient"/> used for making HTTP requests to GraphQL endpoint.
        /// </summary>
        private static HttpClient Client = new HttpClient(); 
        public GqlClient(string Url)
        {
            _url = Url; 
        }
        public async Task<GqlRequestResponse<T>> PostQueryAsync<T>(GqlQuery query)
        {
            
            string queryAsJson = JsonSerializer.Serialize(query, new JsonSerializerOptions(){PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            string queryasJsonWithNoWhiteSpace = WhiteSpaceRemover.RemoveWhiteSpace(queryAsJson);
            StringContent payload = new StringContent(queryasJsonWithNoWhiteSpace, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await Client.PostAsync(_url, payload);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: ${responseMessage.StatusCode}"); 
            }

            string responseAsJson = await responseMessage.Content.ReadAsStringAsync();
            return new GqlRequestResponse<T> { Data = JsonSerializer.Deserialize<T>(responseAsJson, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }) };
        }
    }
}