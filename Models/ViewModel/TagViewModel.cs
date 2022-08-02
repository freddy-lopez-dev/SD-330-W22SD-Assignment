namespace SD_330_W22SD_Assignment.Models.ViewModel
{
    public class TagViewModel
    {
        public List<Question> relatedQuestion { get; set; } = new List<Question>();
        public Tag CurrTag { get; set; }

        public TagViewModel(int Id, List<Question> questions, List<QuestionTag> questionTag, List<Tag> tags)
        {
            CurrTag = tags.First(t => t.Id == Id);
            questionTag.ForEach(qt =>
            {
                if (qt.TagId == Id)
                {
                    relatedQuestion.Add(questions.First(q => q.Id == qt.QuestionId));
                }
            });
        }

    }
}
