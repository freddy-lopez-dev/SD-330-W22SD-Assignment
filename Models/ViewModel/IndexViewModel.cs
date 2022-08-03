using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Mvc;

namespace SD_330_W22SD_Assignment.Models.ViewModel
{
    public class IndexViewModel
    {
        public X.PagedList.IPagedList<Question> SortedQuestions { get; set; }
        public List<SelectListItem> SortOption { get; set; } = new List<SelectListItem>() { new SelectListItem { Value = "1", Text = "Latest" }, new SelectListItem { Value = "2", Text = "Most Answered" } };

        public IndexViewModel(string sortVal, List<Question> questions, int? page)
        {
            if(sortVal == null)
            {
                SortedQuestions = questions.ToPagedList(page ?? 1, 10);
            }
            if(sortVal == "1")
            {
                SortedQuestions = questions.OrderByDescending(q => q.CreatedDate).ToList().ToPagedList(page ?? 1, 10);
            }
            if(sortVal == "2")
            {
                SortedQuestions = questions.OrderByDescending(q => q.Answers.Count()).ToList().ToPagedList(page ?? 1, 10);
            }
            

        }
    }
}
