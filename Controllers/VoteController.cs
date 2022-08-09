using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> VoteUpQuestionAsync(int? id, int? pageId)
        {
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            Vote checkVote = _context.Vote.FirstOrDefault(v => v.Question.Id == id && v.User.UserName == userName);
            if(checkVote == null)
            {
                Question currentQuestion = _context.Question.First(q => q.Id == id);
                ApplicationUser propsToUser = _context.Users.First(a => a.Id == currentQuestion.UserId);
                propsToUser.Reputation += 5;
                if (propsToUser.Reputation == 0)
                {
                    propsToUser.Reputation += 5;
                }
                currentQuestion.VoteCtr += 1;
                if(currentQuestion.VoteCtr == 0)
                {
                    currentQuestion.VoteCtr += 1;
                }
                Vote newVote = new Vote();
                newVote.Question = currentQuestion;
                newVote.User = currUser;
                newVote.VoteType = true;
                currentQuestion.Votes.Add(newVote);
                
                currUser.Votes.Add(newVote);
                _context.SaveChanges();
                if(pageId == 0)
                {
                    return RedirectToAction("Index", "Questions");
                } else
                {
                    return RedirectToAction("Details", "Questions", new { id });
                }
                
            } else
            {
                Question currentQuestion = _context.Question.First(q => q.Id == id);
                ApplicationUser propsToUser = _context.Users.First(a => a.Id == currentQuestion.UserId);
                propsToUser.Reputation += 5;
                if (propsToUser.Reputation == 0)
                {
                    propsToUser.Reputation += 5;
                }
                currentQuestion.VoteCtr += 1;
                if (currentQuestion.VoteCtr == 0)
                {
                    currentQuestion.VoteCtr += 1;
                }
                checkVote.VoteType = true;
                _context.SaveChanges();
                if (pageId == 0)
                {
                    return RedirectToAction("Index", "Questions");
                }
                else
                {
                    return RedirectToAction("Details", "Questions", new { id });
                }
            }
        }

        public async Task<IActionResult> VoteDownQuestionAsync(int? id, int? pageId)
        {
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            Vote checkVote = _context.Vote.FirstOrDefault(v => v.Question.Id == id && v.User.UserName == userName);
            if (checkVote == null)
            {
                Question currentQuestion = _context.Question.First(q => q.Id == id);
                ApplicationUser propsToUser = _context.Users.First(a => a.Id == currentQuestion.UserId);
                propsToUser.Reputation -= 5;
                if (propsToUser.Reputation == 0)
                {
                    propsToUser.Reputation -= 5;
                }
                currentQuestion.VoteCtr -= 1;
                if (currentQuestion.VoteCtr == 0)
                {
                    currentQuestion.VoteCtr -= 1;
                }
                Vote newVote = new Vote();
                newVote.Question = currentQuestion;
                newVote.User = currUser;
                newVote.VoteType = false;
                currentQuestion.Votes.Add(newVote);
                currUser.Votes.Add(newVote);
                _context.SaveChanges();
                if (pageId == 0)
                {
                    return RedirectToAction("Index", "Questions");
                }
                else
                {
                    return RedirectToAction("Details", "Questions", new { id });
                }
            }
            else
            {
                Question currentQuestion = _context.Question.First(q => q.Id == id);
                ApplicationUser propsToUser = _context.Users.First(a => a.Id == currentQuestion.UserId);
                propsToUser.Reputation -= 5;
                if (propsToUser.Reputation == 0)
                {
                    propsToUser.Reputation -= 5;
                }
                currentQuestion.VoteCtr -= 1;
                if (currentQuestion.VoteCtr == 0)
                {
                    currentQuestion.VoteCtr -= 1;
                }
                checkVote.VoteType = false;
                _context.SaveChanges();
                if (pageId == 0)
                {
                    return RedirectToAction("Index", "Questions");
                }
                else
                {
                    return RedirectToAction("Details", "Questions", new { id });
                }
            }
        }

        public async Task<IActionResult> VoteUpAnswerAsync(int? id, int? questionId)
        {
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            Vote checkVote = _context.Vote.FirstOrDefault(v => v.Answer.Id == id && v.User.UserName == userName);
            if (checkVote == null)
            {
                Answer currentAnswer = _context.Answer.First(a => a.Id == id);
                ApplicationUser propsToUser = _context.Users.First(a => a.Id == currentAnswer.UserId);
                propsToUser.Reputation += 5;
                if (propsToUser.Reputation == 0)
                {
                    propsToUser.Reputation += 5;
                }
                currentAnswer.VoteCtr += 1;
                if(currentAnswer.VoteCtr == 0)
                {
                    currentAnswer.VoteCtr += 1;
                }
                Vote newVote = new Vote();
                newVote.Answer = currentAnswer;
                newVote.User = currUser;
                newVote.VoteType = true;
                currentAnswer.Votes.Add(newVote);
                currUser.Votes.Add(newVote);
                _context.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id = questionId });
            }
            else
            {
                Answer currentAnswer = _context.Answer.First(a => a.Id == id);
                ApplicationUser propsToUser = _context.Users.First(a => a.Id == currentAnswer.UserId);
                propsToUser.Reputation += 5;
                if (propsToUser.Reputation == 0)
                {
                    propsToUser.Reputation += 5;
                }
                currentAnswer.VoteCtr += 1;
                if (currentAnswer.VoteCtr == 0)
                {
                    currentAnswer.VoteCtr += 1;
                }
                checkVote.VoteType = true;
                _context.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id = questionId });
            }
        }
        public async Task<IActionResult> VoteDownAnswerAsync(int? id, int? questionId)
        {
            string userName = User.Identity.Name;
            ApplicationUser currUser = await _userManager.FindByNameAsync(userName);
            Vote checkVote = _context.Vote.FirstOrDefault(v => v.Answer.Id == id && v.User.UserName == userName);
            if (checkVote == null)
            {
                Answer currentAnswer = _context.Answer.First(a => a.Id == id);
                ApplicationUser propsToUser = _context.Users.First(a => a.Id == currentAnswer.UserId);
                propsToUser.Reputation -= 5;
                if (propsToUser.Reputation == 0)
                {
                    propsToUser.Reputation -= 5;
                }
                currentAnswer.VoteCtr -= 1;
                if (currentAnswer.VoteCtr == 0)
                {
                    currentAnswer.VoteCtr -= 1;
                }
                Vote newVote = new Vote();
                newVote.Answer = currentAnswer;
                newVote.User = currUser;
                newVote.VoteType = false;
                currentAnswer.Votes.Add(newVote);
                currUser.Votes.Add(newVote);
                _context.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id = questionId });
            }
            else
            {
                Answer currentAnswer = _context.Answer.First(a => a.Id == id);
                ApplicationUser propsToUser = _context.Users.First(a => a.Id == currentAnswer.UserId);
                propsToUser.Reputation -= 5;
                if (propsToUser.Reputation == 0)
                {
                    propsToUser.Reputation -= 5;
                }
                currentAnswer.VoteCtr -= 1;
                if (currentAnswer.VoteCtr == 0)
                {
                    currentAnswer.VoteCtr -= 1;
                }
                checkVote.VoteType = false;
                _context.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id = questionId });
            }
        }
    }
}
