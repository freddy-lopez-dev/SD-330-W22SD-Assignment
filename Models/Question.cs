namespace SD_330_W22SD_Assignment.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Today;
        public string? UserId { get; set; }
        public ApplicationUser Owner { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<QuestionTag> QuestionTags { get; set; }
    }
}
