using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_330_W22SD_Assignment.Data;
using SD_330_W22SD_Assignment.Models;
using SD_330_W22SD_Assignment.Models.ViewModel;
using System.Linq;

namespace SD_330_W22SD_Assignment.Controllers
{
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> TagQuestion(int Id)
        {
            TagViewModel TVM = new TagViewModel(
                Id, 
                _context.Question
                .Include(q => q.Answers)
                          .Include(q => q.Owner)
                          .Include(q => q.QuestionTags)
                          .ThenInclude(t => t.Tag).ToList(),
                _context.QuestionTag.ToList(),
                _context.Tag.ToList());
            return View(TVM);
        }
    }
}
