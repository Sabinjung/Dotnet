using LearnDotNet.Controllers.Dto;
using LearnDotNet.Models;
using LearnDotNet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnDotNet.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NoteController : ControllerBase
    {
        private readonly CrudDBContext DbContext;
        private readonly IMailSender SendMail;
        public NoteController(CrudDBContext dbContext, IMailSender sendMail)
        {
            DbContext = dbContext;
            SendMail = sendMail;
        }

        [HttpGet("{UserId}")]

        public async Task<ActionResult> GetNote( int UserId)
        {
            var user = DbContext.Users.Where(x => x.Id == UserId).FirstOrDefault();
            if (user == null)
            {
                return NotFound("User Doesn't Exists.");
            }
            var result = await DbContext.Notes.Where(x => x.UserId == UserId).Select(x => new GetNotesDto
            {
                Id = x.Id,
                UserId = x.UserId,
                Title = x.Title,
                Description = x.Description,
                CreatedDate = x.CreatedDate
            }).ToListAsync();
 
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> PostNote([FromBody] GetNotesDto note)
        {
            var user = DbContext.Users.Where(x => x.Id == note.UserId).FirstOrDefault();
            if(user == null)
            {
                return NotFound("User Doesn't Exists.");
            }
            var data = new Note
            {
                UserId = note.UserId,
                Title = note.Title,
                Description = note.Description,
                CreatedDate = DateTime.Now
            };
            var greet = SendMail.SendMail(user.Email,data.Title);

            DbContext.Notes.Add(data);
            await DbContext.SaveChangesAsync();
            return Ok("Note Added Successfully");
        }
    }
}
