using PearsonAssesment.Models;
using PearsonAssesment.Repositories;
using PearsonAssesment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearsonAssesment.Commands
{
    public class FollowCommand: Command
    {
        private readonly IUserRepository _userRepository;
        public FollowCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public override string Execute(string commandLine)
        {
            string user = GetUser(commandLine);
            string userFollowing = GetUserFollowing(commandLine);

            if (!CheckUserExists(userFollowing))
            {
                return $"Cannot follow user @{userFollowing}. User not found";
            }
            return AddFollower(user, userFollowing);
        }

        private string AddFollower(string user, string userFollowing)
        {
            User userEntity = _userRepository.GetByUsernameWithFollowers(user);

            if (userEntity.FollowersList.Where(x => x.Username == userFollowing).Count() > 0)
            {
                return $"{user} is already following @{userFollowing}";
            }
            else
            {
                _userRepository.Update(new User { Username = userFollowing, ParentUserId = user });
                return $"{user} is now following @{userFollowing}";
            }
        }

        private string GetUser(string commandLine)
        {
            return commandLine.Split(' ')[1].Split('@')[1];
        }

        private string GetUserFollowing(string commandLine)
        {
            return commandLine.Split(' ')[2].Split('@')[1];
        }

        private bool CheckUserExists(string user)
        {
            return _userRepository.GetById(user) != null;
        }

    }
}
