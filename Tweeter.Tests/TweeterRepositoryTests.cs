using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using Moq;
using System.Data.Entity;
using Tweeter.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tweeter.Tests
{
    
    [TestClass]
    public class TweeterRepositoryTests
    {
        private Mock<DbSet<ApplicationUser>> mock_users { get; set; }
        //mock DbContext
        private Mock<TweeterContext> mock_context { get; set; }
        private TweeterRepository Repo { get; set; }

        private List<ApplicationUser> users { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<TweeterContext>();
            mock_users = new Mock<DbSet<ApplicationUser>>();
            Repo = new TweeterRepository(mock_context.Object); 
            /*install Identity thru Nuget into TweeterTEsts 
            create a mock context that uses UserManager instead of Tweeter Context    mockcontext.Setup in ConnectToDataStore
             * /
             * if we just add a username filed to twit model
             * mock_context.Setup(c => c.TweeterUsers).Returns(mock_users.Object); Assuming mock_users is List<Twit>
             */
        }
        public void ConnectToDataStore()
        {
            users = new List<ApplicationUser>();
            var query_users = users.AsQueryable();

            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator).Returns(query_users.GetEnumerator);

            //Below mocks the Users getter that returns a list of ApplicationUsers
            //mock_user_manager_context.Setup(char => char.Users).Returens(mock_users.Object);

        }

        
        [TestMethod]
        public void RepoCanEnsureInstance()
        {
            TweeterRepository repo = new TweeterRepository();
            Assert.IsNotNull(repo);
        }      
    }
    
}
