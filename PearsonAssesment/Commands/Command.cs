using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PearsonAssesment.Commands
{
    public enum CommandType
    {
        Post = 0,
        Follow = 1,
        Wall = 2
    }
    public abstract class Command
    {
        public abstract string Execute(string commandLine);
    }
}
