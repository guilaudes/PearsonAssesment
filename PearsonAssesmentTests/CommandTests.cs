using Moq;
using PearsonAssesment.Commands;
using PearsonAssesment.Models;
using PearsonAssesment.Repositories.Interfaces;
using System;
using System.Reflection.Metadata;

namespace PearsonAssesmentTests
{
    public class CommandTests
    {
        [Theory]
        [InlineData("post @Bob Hello world", "Bob posted -> 'Hello world' @#currentTime")]
        [InlineData("post @Bob Bye bye", "Bob posted -> 'Bye bye' @#currentTime")]
        [InlineData("post @Hank Today could be a great day", "Hank posted -> 'Today could be a great day' @#currentTime")]
        [InlineData("post @Hank Home now, I’m starving", "Hank posted -> 'Home now, I’m starving' @#currentTime")]
        public void PostCommandTest(string commandLine, string expected)
        {
            // Arrange  
            DateTime fechaActual = DateTime.Now;

            expected = expected.Replace("#currentTime", $"{fechaActual.ToShortTimeString()}");

            var mockPostRepository = new Mock<IPostRepository>();
            var postCommand = new PostCommand(mockPostRepository.Object);

            mockPostRepository.Setup(x => x.Add(It.IsAny<Post>())).Returns(new Post());

            // Act
            var result = postCommand.Execute(commandLine);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("follow @Claudia @Hank", true, false, "Hank", "Claudia is now following @Hank")]
        [InlineData("follow @Claudia @Fonso", false, false, "Fonso", "Cannot follow user @Fonso. User not found")]
        [InlineData("follow @Claudia @Hank", true, true, "Hank", "Claudia is already following @Hank")]
        [InlineData("follow @Claudia @Bob", true, false, "Bob", "Claudia is now following @Bob")]
        public void FollowCommandTest(string commandLine, bool userFound, bool alreadyFollowing, string userFollowing, string expected)
        {
            // Arrange  
            User userFollowingEntity = new User { Username = "Claudia", FollowersList = alreadyFollowing ? new List<User>() { new User { Username = userFollowing } } : new List<User>() };

            var mockUserRepository = new Mock<IUserRepository>();
            var postCommand = new FollowCommand(mockUserRepository.Object);

            mockUserRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(userFound ? new User() : null);
            mockUserRepository.Setup(x => x.GetByUsernameWithFollowers(It.IsAny<string>())).Returns(userFollowingEntity);

            // Act
            var result = postCommand.Execute(commandLine);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WallCommandTest()
        {
            // Arrange  
            User user = new User
            {
                Username = "Claudia",
                    FollowersList = new List<User>() { 
                        new User { Username = "Bob",
                            PostList = new List<Post> { 
                                new Post { Message = "Hello world", PostTime = new DateTime(2024, 5, 15, 12, 46, 5), Username = "Bob" },
                                new Post { Message = "Bye bye", PostTime = new DateTime(2024, 5, 15, 12, 36, 5), Username = "Bob" }
                            } 
                        },
                        new User { Username = "Hank",
                            PostList = new List<Post> {
                                new Post { Message = "Today could be a great day", PostTime = new DateTime(2024, 5, 15, 12, 26, 5), Username = "Hank" },
                                new Post { Message = "Home now, I'm starving", PostTime = new DateTime(2024, 5, 15, 12, 16, 5), Username = "Hank" }
                            }
                        }

                    }   
            };

            var mockUserRepository = new Mock<IUserRepository>();
            var postCommand = new WallCommand(mockUserRepository.Object);

            mockUserRepository.Setup(x => x.GetByUsernameWithFollowersAndPosts(It.IsAny<string>())).Returns(user);

            // Act
            var result = postCommand.Execute("wall @Claudia");

            // Assert
            Assert.Equal($"'Hello world' @Bob @12:46\n'Bye bye' @Bob @12:36\n'Today could be a great day' @Hank @12:26\n'Home now, I'm starving' @Hank @12:16\n", result);
        }
    }
}