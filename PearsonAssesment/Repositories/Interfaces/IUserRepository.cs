using PearsonAssesment.Models;

namespace PearsonAssesment.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public User GetByUsernameWithFollowers(string username);
        public User GetByUsernameWithFollowersAndPosts(string username);
    }
}
