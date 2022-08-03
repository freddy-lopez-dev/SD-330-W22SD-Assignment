using Microsoft.AspNetCore.Mvc.Rendering;

namespace SD_330_W22SD_Assignment.Models.ViewModel
{
    public class IndexViewModel
    {
        public List<Question> SortedQuestions { get; set; } = new List<Question>();
        public List<SelectListItem> SortOption { get; set; } = new List<SelectListItem>() { new SelectListItem { Value = "1", Text = "Latest" }, new SelectListItem { Value = "2", Text = "Most Answered" } };

        public IndexViewModel(string sortVal, List<Question> questions)
        {
            if(sortVal == null)
            {
                SortedQuestions = questions;
            }
            if(sortVal == "1")
            {
                SortedQuestions = questions.OrderByDescending(q => q.CreatedDate).ToList();
            }
            if(sortVal == "2")
            {
                SortedQuestions = questions.OrderByDescending(q => q.Answers.Count()).ToList();
            }
            

        }
    }
}
