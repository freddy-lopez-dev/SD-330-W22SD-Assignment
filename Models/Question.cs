namespace SD_330_W22SD_Assignment.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public ApplicationUser Owner { get; set; }

        public ICollection<Answer>? Answers { get; set; }
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        public ICollection<Vote>? Votes { get; set; } = new List<Vote>();
        public ICollection<QuestionTag>? QuestionTags { get; set; } = new HashSet<QuestionTag>();
    }
}
