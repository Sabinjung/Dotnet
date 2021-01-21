using LearnDotNet.Controllers.Dto;
using LearnDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnDotNet.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController: ControllerBase

    {
        private readonly CrudDBContext DbContext;
        public UserController(CrudDBContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var result = await DbContext.Users.Select(x => new GetUserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                Email = x.Email
            }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{Id}")]

        public async Task<ActionResult> GetUser(int Id)
        {
            var user = await DbContext.Users.Where(x => x.Id == Id).Select(x => new GetUserDto {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                Email = x.Email
            }).ToListAsync();
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] GetUserDto user)
        {

            var data = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Email = user.Email
            };

            DbContext.Users.Add(data);
            await DbContext.SaveChangesAsync();
            return Ok("User Added Successfully");
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            var user = DbContext.Users.Where(x => x.Id == Id).FirstOrDefault();
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();
            return Ok("User Deleted Successfully");
        }
    }
}
