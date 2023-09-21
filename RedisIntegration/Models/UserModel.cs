namespace RedisIntegration.Models;

public class UserModel
{
    public string Id { get; set; } = null!;
    
    public long InformationId { get; set; }
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
}