using EFCoreInMemoryDbDemo;
using Microsoft.EntityFrameworkCore;
using PearsonAssesment.Models;
using PearsonAssesment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearsonAssesment.Repositories
{
    internal class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository()
        {
            using (var context = new PearsonAssesmentContext())
            {
                var users = new List<User>
                {
                    new User { Username = "Bob",  },
                    new User { Username = "Hank" },
                    new User { Username = "Claudia" },

                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        public User GetByUsernameWithFollowers(string username)
        {
            using (var context = new PearsonAssesmentContext())
            {
                return context.Set<User>()
                    .Where(u => u.Username == username)
                    .Include(x => x.FollowersList)
                    .FirstOrDefault();
            }
        }

        public User GetByUsernameWithFollowersAndPosts(string username)
        {
            using (var context = new PearsonAssesmentContext())
            {
                return context.Set<User>()
                    .Where(u => u.Username == username)
                    .Include("FollowersList.PostList")
                    .FirstOrDefault();
            }
        }

    }
}
