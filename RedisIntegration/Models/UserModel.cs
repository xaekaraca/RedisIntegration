using System.ComponentModel.DataAnnotations;

namespace RedisIntegration.Models;

public class UserModel
{
    [Required]
    public string Id { get; set; } = null!;
    
    [Required]
    public long InformationId { get; set; }
    
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;
    
    public List<string>? Roles { get; set; }
}