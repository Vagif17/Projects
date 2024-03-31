using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using YuniYol.Interfaces;
using YuniYol.Models;
using YuniYol.Service.Interfaces;
using static BCrypt.Net.BCrypt;

namespace YuniYol.Service.Classes;

internal class UserService : IUserService
{

    public string SentEmail(string Email)
    {

        Random rnd = new Random();

        string VerifyCode = "";

        for (int i = 0; i < 6; i++)
        {
            VerifyCode += rnd.Next(1,10);
        }

        var mail = "YuniYol@outlook.com";
        var password = "vagifca74";



        var smtpClient = new SmtpClient("smtp-mail.outlook.com",587)
        {
            Credentials = new NetworkCredential(mail,password),
            EnableSsl = true,
        };

        smtpClient.Send(mail,Email,"Verify Code",VerifyCode);

        return VerifyCode;
        
    }


    public IPerson Login(string Username, string Password)
    {
        MyDbContext context = new MyDbContext();
        bool checkResult = false;

        List<Users> users = context.Users.ToList();
        List<Administrators> administrators = context.Administrators.ToList();
        List<Moderators> moderators = context.Moderators.ToList();

        IPerson person;



        for (int i = 0; i < users.Count(); i++)
        {
            if (users[i].Name == Username)
            {
                checkResult = Verify(Password, users[i].Password);
                if (checkResult == true) return users[i];
            }
        }

        for (int i = 0; i < administrators.Count(); i++)
        {
            if (administrators[i].Name == Username)
            {
                checkResult = Verify(Password, administrators[i].Password);
                if (checkResult == true) return administrators[i];
            }
        }

        for (int i = 0; i < moderators.Count(); i++)
        {
            if (moderators[i].Name == Username)
            {
                checkResult = Verify(Password, moderators[i].Password);
                if (checkResult == true) return moderators[i];
            }
        }



        MessageBox.Show("Account is not found or password is incorrect");
        return null;
    }




    public bool CheckParametrs(string Username, string Email, string Password, string ConfirmPassword)
    {
        MyDbContext context = new MyDbContext();

        List<Users> Users = context.Users.ToList();

        List<Administrators> Administrators = context.Administrators.ToList();

        List<Moderators> Moderators = context.Moderators.ToList();

        for (int i = 0; i < Users.Count; i++)
        {
            if (Users[i].Name == Username)
            {
                MessageBox.Show("This Username already registered!");
                return false;
            }

            if (Users[i].Email == Email)
            {
                MessageBox.Show("This Email already registered!");
                return false;
            }
        }


        for (int i = 0; i < Administrators.Count; i++)
        {
            if (Administrators[i].Name == Username)
            {
                MessageBox.Show("This Username already registered!");
                return false;
            }

            if (Administrators[i].Email == Email)
            {
                MessageBox.Show("This Email already registered!");
                return false;
            }
        }

        for (int i = 0; i < Moderators.Count; i++)
        {
            if (Moderators[i].Name == Username)
            {
                MessageBox.Show("This Username already registered!");
                return false;
            }

            if (Moderators[i].Email == Email)
            {
                MessageBox.Show("This Email already registered!");
                return false;
            }
        }




        Regex CheckEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
        {
            MessageBox.Show("Fill in all the fields");
            return false;
        }

        if (!CheckEmail.IsMatch(Email))
        {
            MessageBox.Show("Incorrect Email");
            return false;
        }

        if (Password != ConfirmPassword)
        {
            MessageBox.Show("Password and Confirmed Password are not the same");
            return false;
        }


       

        return true;
       
    }

    public void Register(string Username, string Email, string Password)
    {
        MyDbContext context = new MyDbContext();


        Users users = new Users { Name = Username, Email = Email, Password = HashPassword(Password), RegistrationDate = DateTime.Now };

       

        context.Users.Add(users);
        context.SaveChanges();

    }

    public void ReturnAccount(string Email)
    {
        var mail = "YuniYol@outlook.com";
        var password = "vagifca74";


        


        var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            Credentials = new NetworkCredential(mail, password),
            EnableSsl = true,
        };

        smtpClient.Send(mail, Email, "Return Account","");
    }
}
