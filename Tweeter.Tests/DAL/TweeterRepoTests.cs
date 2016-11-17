using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using Moq;
using System.Data.Entity;
using Tweeter.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tweeter.Tests.DAL
{
    [TestClass]
    public class TweeterRepoTests
    {

        private Mock<DbSet<Twit>> mock_users { get; set; }
        private Mock<TweeterContext> mock_context { get; set; }
        private TweeterRepository Repo { get; set; }
        private List<Twit> users { get; set; }
        private List<Tweet> _tweets { get; set; }
        private Mock<DbSet<Tweet>> mock_tweets { get; set;}

        [TestInitialize]
        public void Initialize()
        {
            mock_tweets = new Mock<DbSet<Tweet>>();
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<Twit>>();
            Repo = new TweeterRepository(mock_context.Object);
            users = new List<Twit>
            {
                new Twit {
                    TwitId = 1,
                    BaseUser = new ApplicationUser() { UserName = "michealb"}
                },
                new Twit {
                    TwitId = 2,
                    BaseUser = new ApplicationUser() { UserName = "sallym"}
                }
            };
           
            _tweets = new List<Tweet>
            {
                new Tweet
                {
                    TweetId = 76,
                    Author = new Twit() { TwitId = 8}
                },
                new Tweet {
                    TweetId = 77,
                    Author = new Twit() { TwitId = 9}
                }
            };
            /* 
             1. Install Identity into Tweeter.Tests (using statement needed)
             2. Create a mock context that uses 'UserManager' instead of 'TweeterContext'
             */
        }
        public void ConnectToDatastore()
        {
            var query_users = users.AsQueryable();

            mock_users.As<IQueryable<Twit>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<Twit>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object);
            mock_users.Setup(u => u.Add(It.IsAny<Twit>())).Callback((Twit t) => users.Add(t));
            /*
             * Below mocks the 'Users' getter that returns a list of ApplicationUsers
             * mock_user_manager_context.Setup(c => c.Users).Returns(mock_users.Object);
             * 
             */

            /* IF we just add a Username field to the Twit model
             * mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object); Assuming mock_users is List<Twit>
             */
             //mocking the tweets
            var query_currentTweets = _tweets.AsQueryable();
            mock_tweets.As<IQueryable<Tweet>>().Setup(w => w.Provider).Returns(query_currentTweets.Provider);
            mock_tweets.As<IQueryable<Tweet>>().Setup(w => w.Expression).Returns(query_currentTweets.Expression);
            mock_tweets.As<IQueryable<Tweet>>().Setup(w => w.ElementType).Returns(query_currentTweets.ElementType);
            mock_tweets.As<IQueryable<Tweet>>().Setup(w => w.GetEnumerator()).Returns(query_currentTweets.GetEnumerator());

            mock_context.Setup(r => r.Tweets).Returns(mock_tweets.Object);
            mock_tweets.Setup(i => i.Add(It.IsAny<Tweet>())).Callback((Tweet i) => _tweets.Add(i));
            mock_tweets.Setup(i => i.Remove(It.IsAny<Tweet>())).Callback((Tweet i) => _tweets.Remove(i));
        }

        [TestMethod]
        public void RepoEnsureCanCreateInstance()
        {
            TweeterRepository repo = new TweeterRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureICanGetUsernames()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            List<string> usernames = Repo.GetUsernames();

            // Assert
            Assert.AreEqual(2, usernames.Count);
        }

        [TestMethod]
        public void RepoEnsureUsernameExists()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            bool exists = Repo.UsernameExists("sallym");

            // Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void RepoEnsureUsernameExistsOfTwit()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            Twit found_twit = Repo.UsernameExistsOfTwit("sallym");

            // Assert
            Assert.IsNotNull(found_twit);
        }
        [TestMethod]
        public void EnsureAddTweetWorks()
        {
            // Arrange
            ConnectToDatastore();

            // Act
            Tweet _newTweet = new Tweet() {Message = "Tweet This" };
            Assert.AreEqual(_tweets.Count, 2);
            var tweetAdded = Repo.AddTweet(_newTweet);
            
            // Assert
            Assert.AreEqual(_tweets.Count, 3);
        }
        [TestMethod]
        //***********************look over Jurnell's lecture on below test
        public void EnsureRemoveTweetWorks()
        {
            //Arrange
            ConnectToDatastore();
            //First add a tweet then remove it inside this same test
            //Act
            Assert.AreEqual(_tweets.Count, 3);
            var tweetRemoved = Repo.RemoveTweet(Tweet.Message);
            //Assert
            Assert.AreEqual
        }
        //check Jurnell's branch for this*********************************;
        [TestMethod]
        public void RepoEnsureICanGetTweets()
        {
            //Arr
            //Connect
            //REpo.AddTweet so that there is somethingn in the the database; 
            Repo.AddTweet("sallym", "heyThere");
        }

    }
}
