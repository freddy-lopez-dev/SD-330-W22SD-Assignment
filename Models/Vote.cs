namespace SD_330_W22SD_Assignment.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public bool VoteType { get; set; }
        public ApplicationUser User { get; set; }
        public Question? Question { get; set; }
        public Answer? Answer { get; set; }
    }
}
