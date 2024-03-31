using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuniYol.Models;

internal class Storage
{

    public Storage()
    {
        
    }

    public Storage(Storage storage)
    {
        UserId = storage.UserId;
        User = storage.User;
        ProductId = storage.ProductId;
        Product = storage.Product;
        OrderDate = storage.OrderDate;
        TrackinId = storage.TrackinId;
    }

    [Key]
    public int Id { get; set; }

    [ForeignKey("Users")]
    public int UserId { get; set; }
    public Users User { get; set; }

    [ForeignKey("Products")]
    public int ProductId { get; set; }
    public Products Product { get; set; }


    [Required]
    public string TrackinId { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }
    public override string ToString()
    {
        return $"🆔 : {Id}   👤🆔 : {UserId}  📦🆔 : {ProductId}     📅 : {OrderDate}     Tracking 🆔 : {TrackinId} ";
    }

}
