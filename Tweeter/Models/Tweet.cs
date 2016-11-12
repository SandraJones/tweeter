using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweeter.Models
{
    public class Tweet
    {
        public int TweetId { get; set; }
        public string Message { get; set; }
        public Twit Author { get; set; } // How do I say 'Twit User'?
        public string ImageURL { get; set; }//hint for what I will store in database; store url not actual image
        public DateTime CreatedAt { get; set; }
        public List<Tweet> Replies { get; set; } // Self referential
    }
}