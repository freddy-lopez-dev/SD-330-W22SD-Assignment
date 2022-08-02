using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SD_330_W22SD_Assignment.Data;
using SD_330_W22SD_Assignment.Models;

namespace SD_330_W22SD_Assignment.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentOnQuestionAsync(int Id, string comment)
        {
            Question currQuestion = _context.Question.First(q => q.Id == Id);
            Comment newComment = new Comment();
            newComment.Content = comment;
            newComment.Question = currQuestion;
            currQuestion.Comments.Add(newComment);
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            newComment.Owner = currUser;
            currUser.Comments.Add(newComment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Questions", new { Id });
        }
    }
}
