using System;

namespace API.DTOs;

public record UserDto
{
    public required Guid Id { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public string? ImageUrl { get; set; }
    public string? Token { get; set; }
}
