/**/

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CatQL.GraphQL.QueryResponses;
using CatQL.GraphQL.Helpers;

//TODO: Rewrite how logging is handled. 
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
        /// <summary>
        /// Enables some simple debugging messages with Console.WriteLine
        /// </summary>
        /// <value>True if logging should be enabled, else false.</value>
        public bool EnableLogging { get; set; }



        /// <summary>
        /// Creates a new GqlClient instance.
        /// </summary>
        /// <param name="Url">The Url that the requests will target</param>
        public GqlClient(string Url)
        {
            _url = Url;
        }

        /// <inheritdoc />

        public async Task<GqlRequestResponse<T>> PostQueryAsync<T>(GqlQuery query)
        {

            string queryAsJson = JsonConvert.SerializeObject(query);
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false },
            };
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ContractResolver = contractResolver
            };

            if (EnableLogging)
            {
                System.Console.WriteLine($"Serializing Query: {WhiteSpaceRemover.RemoveWhiteSpace(queryAsJson)}");
            }

            StringContent payload = new StringContent(queryAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await Client.PostAsync(_url, payload);
            if (!responseMessage.IsSuccessStatusCode)
            {
                string errorResponseMessageAsJson = await responseMessage.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<GqlRequestResponse<T>>(errorResponseMessageAsJson, jsonSettings);
                if (EnableLogging)
                {
                    System.Console.WriteLine($"Error response message: {errorResponseMessageAsJson}");
                    System.Console.WriteLine($"Response Object: {JsonConvert.SerializeObject(responseObject)}");
                }
            }

            string responseAsJson = await responseMessage.Content.ReadAsStringAsync();
            if (EnableLogging)
            {
                System.Console.WriteLine($"Received Response: {responseAsJson}");
            }

            if (EnableLogging)
            {
                System.Console.WriteLine("Deserializing Response...");
            }
            return JsonConvert.DeserializeObject<GqlRequestResponse<T>>(responseAsJson, new JsonSerializerSettings() { ContractResolver = contractResolver });
        }
    }
}