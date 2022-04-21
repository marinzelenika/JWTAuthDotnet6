using JWTAuthDotnet6.Models;

namespace JWTAuthDotnet6.DTOs
{
    public class AddPostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
