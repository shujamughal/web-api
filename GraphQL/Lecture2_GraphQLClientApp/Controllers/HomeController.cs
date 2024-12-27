using System.Diagnostics;
using Lecture2_GraphQLClientApp.Models;
using Lecture2_GraphQLClientApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lecture2_GraphQLClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly GraphQLService _graphQLService;

        public HomeController(GraphQLService graphQLService)
        {
            _graphQLService = graphQLService;
        }

        public async Task<IActionResult> Index()
        {
            var query = @"query {
            posts {
                id
                title
                content
                author {
                    name
                }
                comments {
                    id
                    text
                }
            }
        }";

            var posts = await _graphQLService.ExecuteQueryAsync<List<Post>>(query);
            return View(posts);
        }
    }
}
