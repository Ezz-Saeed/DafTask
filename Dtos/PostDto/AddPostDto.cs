namespace DafTask.Dtos.PostDto
{
    public class AddPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
