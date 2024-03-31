using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Interfaces;

namespace YuniYol.Models;

internal class Moderators : IPerson
{
    public Moderators()
    {

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
}
