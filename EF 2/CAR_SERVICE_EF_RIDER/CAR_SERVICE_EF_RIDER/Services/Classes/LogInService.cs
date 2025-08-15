using System.Windows;
using CAR_SERVICE_EF_RIDER.Interfaces;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using static BCrypt.Net.BCrypt; //??

namespace CAR_SERVICE_EF_RIDER.Services.Classes;

public class LogInService : ILogInService
{
    private MyDbContext DbContext = new MyDbContext();
    
    public LogInService(){}

    public IPerson LogInUser(string email, string password)
    {
        foreach (var user in DbContext.Users) 
        {
            if (email == user.Email)
            {
                if (Verify(password, user.Password))
                {
                    return user;
                }
                else
                {
                    MessageBox.Show("Incorrect Password!", "ApexAuto", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } 
        } 
        return null;
    }

    public IPerson LogInAdmin(string email,string password){
        foreach (var admin in DbContext.Admins)
        {
            if (email == admin.Email)
            {
                if (password ==
                    admin.Password) // без хеширование пароля потому что админа я создал через sql запрос и пароль вписал вручную
                {
                    return admin;
                }
            }
        }
        MessageBox.Show("Incorrect Email!", "ApexAuto", MessageBoxButton.OK, MessageBoxImage.Error);
        return null;

    }
    
}
