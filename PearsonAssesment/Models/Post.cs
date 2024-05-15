using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PearsonAssesment.Models
{
    [PrimaryKey(nameof(Username), nameof(PostTime))]
    public class Post
    {
        public string Message { get; set; }
        public DateTime PostTime { get; set; }
        public string Username { get; set; }
        public User User { get; set; }
    }
}
