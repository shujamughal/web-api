using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lecture2_GraphQLClientApp.Services
{

    public class GraphQLService
    {
        private readonly HttpClient _httpClient;

        public GraphQLService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> ExecuteQueryAsync<T>(string query, object? variables = null)
        {
            var request = new
            {
                query = query,
                variables = variables
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ensures case insensitivity for JSON properties.
            };

            // Deserialize into the proper structure.
            var graphQLResponse = JsonSerializer.Deserialize<GraphQLResponse<Dictionary<string, T>>>(responseString, options);

            if (graphQLResponse == null || graphQLResponse.Data == null)
            {
                throw new JsonException($"Failed to deserialize GraphQL response: {responseString}");
            }

            // Extract the specific key from the "data" dictionary
            var firstKey = graphQLResponse.Data.Keys.FirstOrDefault();
            if (firstKey == null || !graphQLResponse.Data.ContainsKey(firstKey))
            {
                throw new JsonException("Expected data not found in the GraphQL response.");
            }

            return graphQLResponse.Data[firstKey];
        }

        // Define the GraphQL response structure
        private class GraphQLResponse<T>
        {
            public T Data { get; set; }
            public List<GraphQLError> Errors { get; set; }
        }

        // Define GraphQL error structure (if needed)
        private class GraphQLError
        {
            public string Message { get; set; }
            public Dictionary<string, object> Extensions { get; set; }
        }
    }

}
