using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Interfaces;
using YuniYol.Models;

namespace YuniYol.Service.Interfaces;

internal interface IUserService
{
    public IPerson Login(string Username,string Password);
    public bool CheckParametrs(string Username,string Email ,string Password,string ConfirmPassword);
    public void Register(string Username, string Email, string Password);
    public string SentEmail(string Email);
    public void ReturnAccount(string Email);


}
