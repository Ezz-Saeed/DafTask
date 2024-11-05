using Microsoft.AspNetCore.Identity;

namespace DafTask.Models
{
    public class UserProfile : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Post>? Posts { get; set; } = new List<Post>();
    }
}
