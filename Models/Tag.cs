namespace SD_330_W22SD_Assignment.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Emoji { get; set; }
        public string? BgColor { get; set; }

        public ICollection<QuestionTag> QuestionTags { get; set; }

    }
}
