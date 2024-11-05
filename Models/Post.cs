using System.ComponentModel.DataAnnotations.Schema;

namespace DafTask.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        [ForeignKey(nameof(Profile))]
        public string UserId { get; set; }
        public UserProfile? Profile { get; set; }
    }
}
