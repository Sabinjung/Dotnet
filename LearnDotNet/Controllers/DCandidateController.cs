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
    [Route("api/[controller]/{action}")]
    public class DcandidateController : ControllerBase
    {
        private readonly DonationDBContext DbContext; //DB object instance create gare
        public DcandidateController(DonationDBContext dbContext )
        {
            DbContext = dbContext; // feature injection
        }

        // Get: api/DCandidate/GetDCandidates
        [HttpGet]
        public async Task<ActionResult> GetDCandidates()
        {
            var result = await DbContext.DCandidates.Select(x => new { x.FullName, x.Age, x.BloodGroup }).ToListAsync();
            return Ok(result);
        }

        [HttpGet]

        public async Task<List<GetCandidateDto>> GetCandidate()
        {
            var result = await DbContext.DCandidates.Select(x => new GetCandidateDto
            {
                id = x.id,
                FullName = x.FullName,
            }).ToListAsync();
            return result;
        }

        // Get: api/DCandidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDCandidate(int id)
        {
            var dcandidate = await DbContext.DCandidates.FindAsync(id);    
            if (dcandidate == null)
            {
                return NotFound(false);
            }
            return Ok(dcandidate);
        }

        //// Post: api/DCandidate
        [HttpPost]
        public async Task<ActionResult> PostDCandidate([FromBody]DCandidate dCandidate)
        {
            DbContext.DCandidates.Add(dCandidate);
            await DbContext.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPut]
        public async Task<ActionResult> PutDCandidate([FromBody] DCandidate dCandidate)
        {
            var data = DbContext.DCandidates.Where(x => x.id == dCandidate.id).FirstOrDefault();
            data.FullName = dCandidate.FullName;
            DbContext.DCandidates.Update(data);
            await DbContext.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDCandidate([FromQuery] int id)
        {
            var data = DbContext.DCandidates.Where(x => x.id == id).FirstOrDefault();
            DbContext.DCandidates.Remove(data);
            await DbContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
