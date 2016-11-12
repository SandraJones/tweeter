using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Tweeter.DAL
{
    public class TweeterRepository
    {
        public TweeterContext Context { get; set; }
        public TweeterRepository()
        {
            Context = new TweeterContext();
        }
        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        [HttpGet]
        public List<string> GetUsernames()
        {
            int z = 1;
            //what do I have access to and how do I get what I want
            return Context.TweeterUsers.Select(x => x.BaseUser.UserName).ToList();  
        }
        
    }
}