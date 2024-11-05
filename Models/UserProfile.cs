using Microsoft.AspNetCore.Identity;

namespace DafTask.Models
{
    public class UserProfile : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Post>? Posts { get; set; }
    }
}
