using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Member
{
    public Guid Id { set; get; }
    public DateOnly DateOfBirth { set; get; }
    public string? ImageUrl { set; get; }
    public required string DisplayName { set; get; }
    public DateTime Created { set; get; } = DateTime.UtcNow;
    public DateTime LastActive { set; get; }= DateTime.UtcNow;
    public required string Gender { set; get; }
    public string? Description { set; get; }
    public required string City { get; set; }
    public required string Country { get; set; }

    // Navigation property
    [JsonIgnore]
    public List<Photo> Photos { get; set; } = [];
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public AppUser User { get; set; } = null!;
}
