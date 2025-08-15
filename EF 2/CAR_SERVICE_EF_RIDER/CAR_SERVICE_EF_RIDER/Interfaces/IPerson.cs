using System.ComponentModel.DataAnnotations;

namespace CAR_SERVICE_EF_RIDER.Interfaces;

public interface IPerson
{
     [Key] int Id { get; set; }
     
     [Required] string Name { get; set; }
     
     [EmailAddress,Required] string Email { get; set; }

     [Required,MinLength(6)] string Password { get; set; }
     
     
}