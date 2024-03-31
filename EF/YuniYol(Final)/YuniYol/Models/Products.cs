using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuniYol.Models;

internal class Products
{
    public Products()
    {
        
    }

    [Key]
    public int Id { get; set; }


    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public float Price { get; set; }

    [Required]
    public DateTime DateOfIssue { get; set; }

    [Required]
    public DateTime ExpirationDate { get; set; }

    public ICollection<Storage> Storages { get; set; }
    public override string ToString()
    {
        return $"🆔 : {Id}     📦 : {Name}     Description : {Description}      🏷️ : {Price}      📅Date Of Issue : {DateOfIssue}     📅Expiration Date : {ExpirationDate}"; 
    }

}
