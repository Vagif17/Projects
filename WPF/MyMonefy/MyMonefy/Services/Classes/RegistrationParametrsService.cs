using MyMonefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyMonefy.Services.Classes;

internal class RegistrationParametrsService : IRegistrationParametrsService
{
    Regex regexNames = new Regex("[a-zA-Z]{3,}");
    Regex regexEmail = new Regex("\\S+@\\S+\\.\\S+");



    public bool CheckAll(string Name, string Surname, string Email, string Password, string ConfirmPassword)
    {
        return regexNames.IsMatch(Name) && regexEmail.IsMatch(Email) && Password == ConfirmPassword;
    }
}
