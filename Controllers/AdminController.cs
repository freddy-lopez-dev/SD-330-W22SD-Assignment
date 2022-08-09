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
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        
        public async Task<IActionResult> Index()
        {
              return _context.Question != null ? 
                          View(await _context.Question.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Question'  is null.");
        }

        // GET: Admin/Delete/5
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

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Question == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Question'  is null.");
            }
            var question = await _context.Question.FindAsync(id);
            var questionTag = await _context.QuestionTag.FirstAsync(qt => qt.QuestionId == id);
            List<Answer> updatedAnswerList = _context.Answer.Where(a => a.QuesitonId == id).ToList();
            updatedAnswerList.ForEach(ua =>
            {
                _context.Answer.Remove(ua);
                List<Comment> updatedCommentList = _context.Comment.Where(c => c.Answer == ua).ToList();
                updatedCommentList.ForEach(uc => { _context.Comment.Remove(uc); });
                List<Vote> updatedVoteList = _context.Vote.Where(v => v.Answer == ua).ToList();
                updatedVoteList.ForEach(uv => { _context.Vote.Remove(uv); });
            });
            List<Comment> questionComments = _context.Comment.Where(c => c.Question == question).ToList();
            questionComments.ForEach(qc => { _context.Comment.Remove(qc); });
            List<Vote> questionVote = _context.Vote.Where(v => v.Question == question).ToList();
            questionVote.ForEach(qv => { _context.Vote.Remove(qv); });
            if (question != null && questionTag != null)
            {
                _context.Question.Remove(question);
                _context.QuestionTag.Remove(questionTag);
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
