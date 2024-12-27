using System.Xml.Linq;

namespace Lecture2_GraphQLClientApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
