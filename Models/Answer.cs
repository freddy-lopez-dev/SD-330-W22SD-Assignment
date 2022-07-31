namespace SD_330_W22SD_Assignment.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int QuesitonId { get; set; }
        public Question? Question { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
