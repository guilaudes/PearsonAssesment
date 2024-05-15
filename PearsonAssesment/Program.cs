using EFCoreInMemoryDbDemo;
using Microsoft.Extensions.DependencyInjection;
using PearsonAssesment.Commands;
using PearsonAssesment.Repositories;
using PearsonAssesment.Repositories.Interfaces;
using System;

namespace PearsonAssesment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
            .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IPostRepository, PostRepository>();

            var serviceProvider = services.BuildServiceProvider();

            var postRepository = serviceProvider.GetRequiredService<IPostRepository>();
            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            
            CommandFactory commandFactory = new CommandFactory(userRepository, postRepository);

            while (true)
            {
                string commandLine = Console.ReadLine();
                CommandType commandType = GetCommandType(commandLine);
                Command command = commandFactory.GetCommandBasedOnType(commandType);

                Console.WriteLine(command.Execute(commandLine));
            }
            
        }

        private static CommandType GetCommandType(string commandLine)
        {
            return (CommandType)Enum.Parse(typeof(CommandType), commandLine.Split(' ')[0], true);
        }
    }
}
