using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_330_W22SD_Assignment.Data;
using SD_330_W22SD_Assignment.Models;
using SD_330_W22SD_Assignment.Models.ViewModel;

namespace SD_330_W22SD_Assignment.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Questions
        public async Task<IActionResult> Index(string? sortVal, int? page)
        {
            IndexViewModel IVM = new IndexViewModel(sortVal, await _context.Question
                          .Include(q => q.Answers)
                          .Include(q => q.Owner)
                          .Include(q => q.QuestionTags)
                          .ThenInclude(t => t.Tag)
                          .ToListAsync(), page);
            return View(IVM);
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            DetailsViewModel DVM = new DetailsViewModel(
                id, _context.Question
                .Include(q => q.Answers)
                          .Include(q => q.Owner)
                          .Include(q => q.Comments)
                          .Include(q => q.QuestionTags)
                          .ThenInclude(t => t.Tag).ToList(),
                _context.Answer.Include(a => a.User).Include(a => a.Comments).ToList());
            return View(DVM);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            List<SelectListItem> currTags = new List<SelectListItem>();
            _context.Tag.ToList().ForEach(t =>
            {
                currTags.Add(new SelectListItem(t.Name, t.Id.ToString()));
            });
            CreatePostViewModel CPVM = new CreatePostViewModel()
            {
                Tags = currTags
            };

            return View(CPVM);
        }

        public async Task<IActionResult> PostAnswerAsync(int id, string answerContent)
        {
            Question currQuestion = _context.Question.Include(q => q.Answers).First(q => q.Id == id);
            Answer newAnswer = new Answer();
            newAnswer.Question = currQuestion;
            newAnswer.QuesitonId = currQuestion.Id;
            newAnswer.Content = answerContent;
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            newAnswer.User = currUser;
            newAnswer.UserId = currUser.Id;
            currQuestion.Answers.Add(newAnswer);
            currUser.Answers.Add(newAnswer);
            _context.Answer.Add(newAnswer);
            _context.SaveChanges();
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestionAsync(IEnumerable<int> SelectedTags, string title, string bodyContent)
        {
            Question newQuestion = new Question();
            newQuestion.Title = title;
            newQuestion.Body = bodyContent;
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            List<Tag> listOfTags = new List<Tag>();
            listOfTags = _context.Tag.Where(t => SelectedTags.Contains(t.Id)).ToList();
            newQuestion.UserId = currUser.Id;
            newQuestion.Owner = currUser;
            listOfTags.ForEach(l =>
            {
                QuestionTag newQT = new QuestionTag();
                newQT.QuestionId = newQuestion.Id;
                newQT.TagId = l.Id;
                newQT.Tag = l;
                newQT.Question = newQuestion;
                newQuestion.QuestionTags.Add(newQT);
                _context.QuestionTag.Add(newQT);
            });
            _context.Question.Add(newQuestion);
            currUser.Questions.Add(newQuestion);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Question == null)
            {
                return NotFound();
            }

            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,CreatedDate,UserId")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Question == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Question == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Question'  is null.");
            }
            var question = await _context.Question.FindAsync(id);
            if (question != null)
            {
                _context.Question.Remove(question);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
          return (_context.Question?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
