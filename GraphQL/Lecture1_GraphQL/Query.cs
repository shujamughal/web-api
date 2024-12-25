using Lecture1_GraphQL.Models;

namespace Lecture1_GraphQL
{
    public class Query
    {
        public IEnumerable<Post> GetPosts(string? titleKeyword = null)
        {
            var posts = new List<Post>
        {
            new Post
            {
                Id = 1,
                Title = "Introduction to GraphQL",
                Content = "GraphQL is awesome!",
                Author = new Author { Id = 1, Name = "John Doe" },
                Comments = new List<Comment>
                {
                    new Comment { Id = 1, Text = "Great post!" },
                    new Comment { Id = 2, Text = "Very informative." }
                }
            },
            new Post
            {
                Id = 2,
                Title = "Advanced GraphQL",
                Content = "Let's dive deeper!",
                Author = new Author { Id = 2, Name = "Jane Smith" },
                Comments = new List<Comment>
                {
                    new Comment { Id = 3, Text = "Can't wait for more!" }
                }
            }
        };

            // Apply filtering if a keyword is provided
            if (!string.IsNullOrEmpty(titleKeyword))
            {
                posts = posts.Where(p => p.Title.Contains(titleKeyword, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return posts;
        }

       
    }
}
