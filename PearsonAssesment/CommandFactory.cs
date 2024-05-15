using PearsonAssesment.Commands;
using PearsonAssesment.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearsonAssesment
{
    public class CommandFactory
    {
        private Dictionary<CommandType, Func<Command>> _commandTypeMapper;
        public CommandFactory(IUserRepository userRepository, IPostRepository postRepository)
        {
            _commandTypeMapper = new Dictionary<CommandType, Func<Command>> ();
            _commandTypeMapper.Add(CommandType.Post, () => { return new PostCommand(postRepository); });
            _commandTypeMapper.Add(CommandType.Follow, () => { return new FollowCommand(userRepository); });
            _commandTypeMapper.Add(CommandType.Wall, () => { return new WallCommand(userRepository); });
        }

        public Command GetCommandBasedOnType(CommandType commandType)
        {
            return _commandTypeMapper[commandType]();
        }
    }
}
