using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweeter.Models;

namespace Tweeter.DAL
{
    public class TweeterRepository
    {
        public TweeterContext Context { get; set; }
        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        public TweeterRepository() {}

        public List<string> GetUsernames()
        {
            return Context.TweeterUsers.Select(u => u.BaseUser.UserName).ToList();
        }

        public Twit UsernameExistsOfTwit(string v)
        {
            return Context.TweeterUsers.FirstOrDefault(u => u.BaseUser.UserName.ToLower() == v.ToLower());
        }

        public bool UsernameExists(string v)
        {
            // Works if mocked the UserManager
            /*
            if (Context.Users.Any(u => u.UserName.Contains(v)))
            {
                return true;
            }
            return false;
            */
            
            Twit found_twit = Context.TweeterUsers.FirstOrDefault(u => u.BaseUser.UserName.ToLower() == v.ToLower());
            if (found_twit != null)
            {
                return true;
            }

            return false;          
        }
        public Tweet AddTweet(Tweet currentTweet)
        {
            var _tweet = Context.Tweets.Add(currentTweet);// this is kind of an abstraction of the DB a list
            return _tweet;//when testing check for a tweetID returned 
        }
        public Tweet RemoveTweet(int IdToFind)
        {
            //have to find tweet with this TweetId and then remove it
            //use a linq 
            Tweet found_tweet = Context.Tweets.FirstOrDefault(t => t.TweetId == IdToFind);
            var _tweet = Context.Tweets.Remove(found_tweet);
            return found_tweet;//if I return the same object then I can have proof that it was removed.
        }
    }
}