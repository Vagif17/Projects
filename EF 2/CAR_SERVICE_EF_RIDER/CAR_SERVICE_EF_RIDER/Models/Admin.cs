using System.ComponentModel.DataAnnotations;
using CAR_SERVICE_EF_RIDER.Interfaces;

namespace CAR_SERVICE_EF_RIDER.Models;

public class Admin : IPerson
{
    public Admin()
    {
        
    }

    public Admin(Admin admin)
    {
        Id = admin.Id;
        Name = admin.Name;
        Email = admin.Email;
        Password = admin.Password;
    }
    
    [Key]public int Id { get; set; }
    [Required]public string Name { get; set; }
    [EmailAddress,Required]public string Email { get; set; }
    [Required,MinLength(6)]public string Password { get; set; }
}