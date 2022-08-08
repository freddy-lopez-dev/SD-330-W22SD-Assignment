﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SD_330_W22SD_Assignment.Data;
using SD_330_W22SD_Assignment.Models;

namespace SD_330_W22SD_Assignment.Controllers
{
    public class VoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VoteController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> VoteUpQuestionAsync(int? id)
        {
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            Vote checkVote = _context.Vote.FirstOrDefault(v => v.Question.Id == id && v.User.UserName == userName);
            if(checkVote == null)
            {
                Question currentQuestion = _context.Question.First(q => q.Id == id);
                Vote newVote = new Vote();
                newVote.Question = currentQuestion;
                newVote.User = currUser;
                newVote.VoteType = true;
                currentQuestion.Votes.Add(newVote);
                currUser.Votes.Add(newVote);
                _context.SaveChanges();
                return RedirectToAction("Index", "Questions");
            } else
            {
                checkVote.VoteType = true;
                _context.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }
        }

        public async Task<IActionResult> VoteDownQuestionAsync(int? id)
        {
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            Vote checkVote = _context.Vote.First(v => v.Question.Id == id && v.User.UserName == userName);
            if (checkVote == null)
            {
                Question currentQuestion = _context.Question.First(q => q.Id == id);
                Vote newVote = new Vote();
                newVote.Question = currentQuestion;
                newVote.User = currUser;
                newVote.VoteType = false;
                currentQuestion.Votes.Add(newVote);
                currUser.Votes.Add(newVote);
                _context.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }
            else
            {
                checkVote.VoteType = false;
                _context.SaveChanges();
                return RedirectToAction("Index", "Questions");
            }
        }
        public async Task<IActionResult> VoteUpQuestionDetailsAsync(int? id)
        {
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            Vote checkVote = _context.Vote.FirstOrDefault(v => v.Question.Id == id && v.User.UserName == userName);
            if (checkVote == null)
            {
                Question currentQuestion = _context.Question.First(q => q.Id == id);
                Vote newVote = new Vote();
                newVote.Question = currentQuestion;
                newVote.User = currUser;
                newVote.VoteType = true;
                currentQuestion.Votes.Add(newVote);
                currUser.Votes.Add(newVote);
                _context.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id });
            }
            else
            {
                checkVote.VoteType = true;
                _context.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id });
            }
        }

        public async Task<IActionResult> VoteDownQuestionDetailsAsync(int? id)
        {
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            Vote checkVote = _context.Vote.First(v => v.Question.Id == id && v.User.UserName == userName);
            if (checkVote == null)
            {
                Question currentQuestion = _context.Question.First(q => q.Id == id);
                Vote newVote = new Vote();
                newVote.Question = currentQuestion;
                newVote.User = currUser;
                newVote.VoteType = false;
                currentQuestion.Votes.Add(newVote);
                currUser.Votes.Add(newVote);
                _context.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id });
            }
            else
            {
                checkVote.VoteType = false;
                _context.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id });
            }
        }
    }
}
