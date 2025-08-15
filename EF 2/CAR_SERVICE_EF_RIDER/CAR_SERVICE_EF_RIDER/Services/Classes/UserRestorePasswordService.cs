using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static BCrypt.Net.BCrypt; //??

namespace CAR_SERVICE_EF_RIDER.Services.Classes;

public class UserRestorePasswordService : IUserRestorePasswordService
{
    private MyDbContext DbContext = new MyDbContext();

    public bool Find_User(string Email)
    {

        if (Email == DbContext.Users.Where(u => u.Email == Email).Select(u => u).ToList().First().Email)
        {
            return true;
        }

        MessageBox.Show("Email not found!", "ApexAuto", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }
    
    public bool Check_Parameters(string Email)
    {
        Regex pattern = new Regex("^((?!\\.)[\\w\\-_.]*[^.])(@\\w+)(\\.\\w+(\\.\\w+)?[^.\\W])$");
        
        if (Email == null || pattern.IsMatch(Email) == false)
        {
            MessageBox.Show("Incorrect Email!","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Error);
            return false;
        }

        return true;
    }
    

    public string Send_VerifyCode(string Email)
    {
        Random rnd = new Random();

        string VerifyCode = "";

        for (int i = 0; i < 6; i++)
        {
            VerifyCode += rnd.Next(1, 10);
        }


        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("ApexAutoServiceEmail@gmail.com");
        mail.To.Add(Email);
        mail.Subject = "Restore ApexAuto Account";
        
         
        mail.Body = 
            $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{
            font-family: 'Segoe UI', sans-serif;
            background-color: #f4f4f4;
            padding: 20px;
            color: #000;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            border-radius: 10px;
            padding: 30px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            color: #000;
        }}
        .header {{
            background-color: #007BFF;
            color: #fff;
            padding: 15px;
            border-radius: 8px 8px 0 0;
            text-align: center;
            font-size: 24px;
        }}
        .content {{
            margin-top: 20px;
            font-size: 18px;
            color: #000;
        }}
        .code {{
            margin: 30px auto;
            text-align: center;
            font-size: 36px;
            letter-spacing: 10px;
            font-weight: bold;
            color: #000;
        }}
        .footer {{
            margin-top: 40px;
            font-size: 14px;
            text-align: center;
            color: #666;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            Apex Auto Service
        </div>
        <div class='content'>
            Здравствуйте!<br><br>
            Ваш код подтверждения:
        </div>
        <div class='code'>{VerifyCode}</div>
        <div class='content'>
            Если вы не запрашивали код, просто проигнорируйте это сообщение.
        </div>
        <div class='footer'>
            &copy; 2025 Apex Auto Service
        </div>
    </div>
</body>
</html>";
        mail.IsBodyHtml = true;


        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Port = 587;
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new System.Net.NetworkCredential("ApexAutoServiceEmail@gmail.com", "ocmhknakgrikobbz");

        smtpClient.Send(mail);

        return VerifyCode;

    }

    public void Update_Database(string NewPassword,string ConfirmPassword,string Email)
    {
        if (NewPassword == null || NewPassword.Length < 6 || NewPassword.Length == 0)
        {
            MessageBox.Show("Incorrect Password!","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Error);
        }
        else
        {

            if (NewPassword == null || NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Confrim passwrod is wrong!", "ApexAuto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                
                DbContext.Users.Where(u => u.Email ==  Email).ExecuteUpdate(s => s.SetProperty(d => d.Password, HashPassword(NewPassword))); 
                DbContext.SaveChanges();
            }
        }
    }

}