namespace SD_330_W22SD_Assignment.Models.ViewModel
{
    public class DetailsViewModel
    {
        public Question Question { get; set; }
        public List<Answer> Answer { get; set; } = new List<Answer>();
        public Answer CorrectAnswer { get; set; }
        public List<Vote> Vote { get; set; } = new List<Vote>();

        public DetailsViewModel(int? id, List<Question> questions, List<Answer> answers, List<Vote> votes)
        {
            Question = questions.First(q => q.Id == id);

            Question.Answers.ToList().ForEach(q =>
            {
                Answer.Add(answers.First(a => a.Id == q.Id));
            });

            List<Vote> filteredVote = votes.Where(v => v.Question != null).ToList();
            Vote = filteredVote.Where(v => v.Question.Id == id).ToList();

            Answer.OrderByDescending(a => a.IsCorrect);
            CorrectAnswer = Answer.FirstOrDefault(a => a.IsCorrect == true);
        }
    }
}
