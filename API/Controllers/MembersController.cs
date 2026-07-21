using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")] // locahost:5001/api/members
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.AppUsers.ToListAsync();

            return members;
        }

        [HttpGet("{id}")] // locahost:5001/api/members/bob-id
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.AppUsers.FindAsync(id);

            if (member == null) return NotFound();

            return member;
        }
        [HttpPost]
        public async Task<ActionResult<AppUser>> CreateMember(AppUser user)
        {
            user.Id = Guid.NewGuid();
            context.AppUsers.Add(user);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMember), new { id = user.Id }, user);
        }
    }
}
