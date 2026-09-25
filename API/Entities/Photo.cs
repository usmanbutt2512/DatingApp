using System.Text.Json.Serialization;

namespace API.Entities;

public class Photo
{
    public int Id{set;get;}
    public required string Url{set;get;}
    public string? PublicId{set;get;}

    [JsonIgnore]
    public Member member {set;get;} = null!;
    public Guid MemberId{set;get;} = Guid.Empty; 
}
