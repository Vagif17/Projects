using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonefy.Services.Interfaces;

internal interface IRegistrationParametrsService
{

   
    public bool CheckAll(string Name,string Surname,string Email,string Password,string ConfirmPassword);

}
