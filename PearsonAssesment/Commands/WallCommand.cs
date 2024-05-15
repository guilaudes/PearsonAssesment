using PearsonAssesment.Models;
using PearsonAssesment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearsonAssesment.Commands
{
    public class WallCommand: Command
    {
        private IUserRepository _userRepository;
        public WallCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public override string Execute(string commandLine)
        {
            string username = GetUsername(commandLine);

            User userEntity = _userRepository.GetByUsernameWithFollowersAndPosts(username);

            List<Post> posts = userEntity.FollowersList.SelectMany(x => x.PostList).OrderByDescending(x => x.PostTime).ToList();

            return FormatWallMessages(posts);
        }

        private string GetUsername(string commandLine)
        {
            return commandLine.Split(' ')[1].Split('@')[1];
        }

        private string FormatWallMessages(List<Post> posts)
        {
            string wallMessages = string.Empty;
            foreach (var post in posts)
            {
                wallMessages += $"'{post.Message}' @{post.Username} @{post.PostTime.Hour}:{post.PostTime.Minute}\n";
            }

            return wallMessages;
        }
    }
}
