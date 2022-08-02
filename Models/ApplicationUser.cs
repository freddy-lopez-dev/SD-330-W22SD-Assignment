using Microsoft.AspNetCore.Identity;

namespace SD_330_W22SD_Assignment.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
        public ICollection<Comment> Comments { get; set;} = new HashSet<Comment>();

        public ApplicationUser(): base()
        {

        }
    }
}
