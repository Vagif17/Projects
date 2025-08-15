using System.Text.RegularExpressions;
using System.Windows;
using CAR_SERVICE_EF_RIDER.Interfaces;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using static BCrypt.Net.BCrypt; //??

namespace CAR_SERVICE_EF_RIDER.Services.Classes;

public class UserRegistrationService : IUserRegistrationService
{
    private MyDbContext DbContext = new MyDbContext();
    public UserRegistrationService(){}
    
    public bool Check_Parameters(string email, string name, string password, string passwordconfirm)
    {
        foreach (var user in DbContext.Users )
        {
            if (user.Email == email)
            {
                MessageBox.Show("Person with thi email already registered","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }
        
        foreach (var admin in DbContext.Admins )
        {
            if (admin.Email == email)
            {
                MessageBox.Show("Person with thi email already registered","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }
        
        
        Regex pattern = new Regex("^((?!\\.)[\\w\\-_.]*[^.])(@\\w+)(\\.\\w+(\\.\\w+)?[^.\\W])$");
        
        if (name == null || name.Length < 5 || name.Length == 0)
        {
            MessageBox.Show("Incorrect Username","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Error);
            return false;
        }

        if (email == null || pattern.IsMatch(email) == false)
        {
            MessageBox.Show("Incorrect Email!","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Error);
            return false;
        }
        
        if (password == null || password.Length < 6 || password.Length == 0)
        {
            MessageBox.Show("Incorrect Password!","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Error);
            return false;
        }

        if (password == null || password != passwordconfirm)
        {
            MessageBox.Show("Confrim passwrod is wrong!","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Error);
            return false;
        }

        return true;
    }

    public void Register_User(string email, string name, string password)
    {
        User New_User = new User { Name = name,Email = email,Password = HashPassword(password)};

        DbContext.Users.Add(New_User);
        DbContext.SaveChanges();
        
    }
}