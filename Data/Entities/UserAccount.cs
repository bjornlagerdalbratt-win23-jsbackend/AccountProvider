using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class UserAccount : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    [ProtectedPersonalData]
    public string? Bio { get; set; }

    public string? ProfileImage { get; set; } = "avatar.jpg";

    public int? AddressId { get; set; }
    public UserAddress? Address { get; set; }
    
    public int? CourseId { get; set; }
}
