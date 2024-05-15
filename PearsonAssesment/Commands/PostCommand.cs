using PearsonAssesment.Models;
using PearsonAssesment.Repositories.Interfaces;

namespace PearsonAssesment.Commands
{
    public class PostCommand: Command
    {
        private readonly IPostRepository _postRepository;

        public PostCommand(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public override string Execute(string commandLine)
        {
            string userName = GetUserName(commandLine);
            string message = GetMessage(commandLine);

            Post post = new Post
            {
                Username = userName,
                Message = message,
                PostTime = DateTime.Now
            };

            _postRepository.Add(post);
            return $"{userName} posted -> '{message}' @{post.PostTime.ToShortTimeString()}";
        }

        private string GetUserName(string commandLine)
        {
            return commandLine.Split(' ')[1].Split('@')[1];
        }

        private string GetMessage(string commandLine)
        {
            string result = string.Empty;
            string[] message = commandLine.Split(' ');

            for (var i = 2;i < message.Length; i++)
            {
                result = result + message[i] + (message.Length - 1 != i ? " ": string.Empty);
            }

            return result;
        }
    }
}
