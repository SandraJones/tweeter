using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tweeter.Models
{
    public class TweeterViewModels
    {
        public class TweetViewModel
        {
        //at the point of post don't have a tweetID that is DB generated
            [Required]
            [MinLength(2)]
            public string Message { get; set; }
            public string ImageURL { get; set; }
        //don't have createdAt here because easier to create dateTime now inside a controller 
        }
    }
}