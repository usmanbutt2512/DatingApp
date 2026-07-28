using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extension;

public static class AppUserExtensions
{
     public static UserDto ToDto(this AppUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,             // Token will be generated during login
        };
    }
    public static UserDto ToDto(this AppUser user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenService.CreateToken(user) // Token will be generated during login
        };
    }
    
}
