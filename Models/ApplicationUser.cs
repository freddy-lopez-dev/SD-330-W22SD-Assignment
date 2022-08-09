using Microsoft.AspNetCore.Identity;
using SD_330_W22SD_Assignment.Data;

namespace SD_330_W22SD_Assignment.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
        public ICollection<Comment> Comments { get; set;} = new HashSet<Comment>();
        public ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();
        public int? Reputation { get; set; } = 0;

        public ApplicationUser() : base()
        {
            Reputation = 0;
        }
    }
}
