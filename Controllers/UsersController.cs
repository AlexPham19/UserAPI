#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UserController(UserDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [EnableCors]
        // GET: api/user
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        [EnableCors]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        

        // PUT: api/user/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [EnableCors]
        public ActionResult EditUser(int? id)
        {                       
            if(!userExists(id ?? 0))
            {
                return NotFound();
            }
            // get all current details of the record, then update with User results
            User user = _context.Users.Find(id);
            return Ok(user);
        }
        [HttpPut]
        [EnableCors]
        public async Task<ActionResult> EditUser([Bind(include:"Name,Email")]User user)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost]
        [EnableCors]
        public async Task<ActionResult> CreateUser([Bind(include: "Name,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            return BadRequest();
        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        [EnableCors]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool userExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}