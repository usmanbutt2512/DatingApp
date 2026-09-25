using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;

namespace API.Data;

public class MemberRepository(AppDbContext context) : IMemberRepository
{
    public async Task<Member> Add(Member member)
    {
        var result = await context.Members.AddAsync(member);        
        return result.Entity;
    }

    public async Task<Member?> GetMemberByIdAsync(Guid id)
    {
        return await context.Members.FindAsync(id);        
    }

    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
        return await context.Members.AsNoTracking().ToListAsync();        
    }

    public async Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(Guid memberId)
    {
        return await context.Members.Where(x=>x.Id ==memberId).SelectMany(x=>x.Photos).ToListAsync();
        //return await context.Photos.Where(x=>x.MemberId == memberId).ToListAsync();         
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;          
    }

    public void Update(Member member)
    {
        context.Entry(member).State = EntityState.Modified;
    }
}
