namespace Lecture1_GraphQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add GraphQL services
            builder.Services
                .AddGraphQLServer()
                .AddQueryType<Query>();

            var app = builder.Build();

            app.MapGet("/", () => "Navigate to: https://localhost:7197/graphql/");
            // Use GraphQL
            app.MapGraphQL("/graphql");
            app.Run();
        }
    }
}
