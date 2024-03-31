using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Interfaces;
using YuniYol.Models;

namespace YuniYol.Service.Interfaces;

internal interface IMessageService
{
    public void SendActiveUser(IPerson User);
    public void SendProfilesUsers(MyDbContext Context);
    public void SendVerifyCode(string VerifyCode);
    public void SentInfoToUserRegisterMessage(string Username, string Email, string Password);
    public void SentEmailToReset(string Email);
    public void SentAllProducts(List<Products> products);
    public void SentAllStorages(List<Storage> storages);

}
