using System.Windows.Media.Imaging;
using CAR_SERVICE_EF_RIDER.Models;

namespace CAR_SERVICE_EF_RIDER.Services.Interfaces;

public interface ICreateAdService
{
    public void Create_Ad(string Title, int Price, string Description,byte[] Image, User User);
}