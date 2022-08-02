namespace SD_330_W22SD_Assignment.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public Question? Question { get; set; }
        public Answer? Answer { get; set; }
        public ApplicationUser Owner { get; set; }
    }
}
