using System.ComponentModel.DataAnnotations;
using CAR_SERVICE_EF_RIDER.Interfaces;

namespace CAR_SERVICE_EF_RIDER.Models;

public class User : IPerson
{
    public User() {}
    
    public User(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        Password = user.Password;
    }
    
    [Key] public int Id { get; set; }
    
    [Required] public string Name { get; set; }

    [EmailAddress,Required] public string Email { get; set; }
    
    [Required, MinLength(6)] public string Password { get; set; }

    public ICollection<Item> Items { get; set; }
    
}