using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public UserProfile? Profile { get; set; }
    }
}
