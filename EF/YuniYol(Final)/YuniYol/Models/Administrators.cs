using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Interfaces;

namespace YuniYol.Models;

internal class Administrators : IPerson
{
    public Administrators()
    {

    }

    public Administrators(Users users)
    {
        Name = users.Name;
        Email = users.Email;
        Password = users.Password;
        RegistrationDate = users.RegistrationDate;
    }

    [Key]
    public int Id { get; set; }


    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required,
    MinLength(6)
      ]
    public string Password { get; set; }

    [Required]
    public DateTime RegistrationDate { get; set; }


    public override string ToString()
    {
        return $"🆔 : {Id}   👤 : {Name}  📧 :{Email}  📅 : {RegistrationDate}";
    }
}
