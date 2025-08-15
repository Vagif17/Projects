using System.IO;
using System.Windows.Media.Imaging;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace CAR_SERVICE_EF_RIDER.Services.Classes;

public class CreateAdService : ICreateAdService
{
    private MyDbContext DbContext = new MyDbContext();

    

    public void Create_Ad(string Title, int Price, string Description,byte[] Image ,User User)
    {
        if (Title != null && Price != 0 && Price != null && Description != null)
        {
            DbContext.Items.Add(new Item(){Title = Title,Price = Price,Description = Description,DateTime =DateTime.Today,UserId = User.Id,Image = Image});
            DbContext.SaveChanges();
            
        }
    }

}
