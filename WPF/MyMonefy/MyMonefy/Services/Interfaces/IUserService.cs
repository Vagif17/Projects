using MyMonefy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyMonefy.Services.Interfaces;

interface IUserService
{


    public void Register(Account user);

    public bool Login(string name,string password);
}
