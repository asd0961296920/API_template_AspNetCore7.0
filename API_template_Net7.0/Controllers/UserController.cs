using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TextContext;
using Models;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mm.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly Context _context;

        public UserController(Context context)
        {
            _context = context;
        }

        // GET: api/user
        [HttpGet]
        public object Get()
        {
            var user = _context.User.ToList();
            return user;
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public object? Get(int id)
        {

            var user = _context.User.Find(id);
            return user;
        }

        // POST api/user
        [HttpPost]
        public User Post([FromBody] User user)
        {
            //示範
            //var user2 = new User
            //{
            //    Name = "",
            //    Password = "EF Core",
            //};

            _context.User.Add(user);
            _context.SaveChanges();

            var newUser = _context.User.OrderByDescending(u => u.Id).FirstOrDefault();
            return newUser;

        }

        // PATCH api/user/5
        [HttpPatch("{id}")]
        public User Patch(int id, [FromBody] User user)
        {
            var userData = _context.User.Find(id);

            if (userData != null)
            {
                // 附加新的資料到上下文中，使其受 EF Core 更改跟蹤機制監視
                _context.Entry(userData).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            return userData;
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var user = _context.User.Find(1);
            _context.User.Remove(user);
            _context.SaveChanges();
        }
    }
}

