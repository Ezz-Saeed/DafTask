namespace DafTask.Dtos
{
    public class UpdateUserDto
    {
        public string Email { get; set; }
        public string? NewEmail { get; set; }
        public string? Password { get; set; }
        public string? OldPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
    }
}
