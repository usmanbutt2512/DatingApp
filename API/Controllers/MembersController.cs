using API.Data;
using API.DTOs;
using API.Entities;
using API.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // locahost:5001/api/members    
    [Authorize]
    public class MembersController(AppDbContext context) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetMembers()
        {
            var members = await context.AppUsers.Select(x=>AppUserExtensions.ToDto(x)).ToListAsync();
            
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
