using System;
using API.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace API.Interfaces;

public interface IMemberRepository
{
    void Update(Member member);
    Task<Member> Add(Member member);
    Task<bool> SaveAllAsync();
    Task<IReadOnlyList<Member>> GetMembersAsync();
    Task<Member?> GetMemberByIdAsync(Guid id);
    Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(Guid memberId);
}
