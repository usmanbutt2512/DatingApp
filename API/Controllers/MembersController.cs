using API.Data;
using API.DTOs;
using API.Entities;
using API.Extension;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // locahost:5001/api/members    
    [Authorize]
    public class MembersController(
        IMemberRepository memberRepository
    ) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            var members = await memberRepository.GetMembersAsync();
            
            return Ok(members);
        }

        [HttpGet("{id}")] // locahost:5001/api/members/bob-id
        public async Task<ActionResult<Member>> GetMember(Guid id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id);

            if (member == null) return NotFound();

            return member;
        }

        [HttpGet("{id}/photos")]
        public async Task<ActionResult<IReadOnlyList<Photo>>> GetMemberPhotos(Guid id)
        {
            var photos = await memberRepository.GetPhotosForMemberAsync(id);
            return Ok(photos);
        }

        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember(Member user)
        {
            user.Id = Guid.NewGuid();
            var member= await memberRepository.Add(user);
            await memberRepository.SaveAllAsync();
            return member;
        }
    }
}
