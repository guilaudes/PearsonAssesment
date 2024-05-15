using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PearsonAssesment.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public List<Post> PostList { get; set; }
        public List<User> FollowersList { get; set; }

        public virtual User ParentUser { get; set; }
        public string? ParentUserId { get; set;}

    }
}
