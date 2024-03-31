using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Interfaces;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.Service.Classes;
internal class MessageService : IMessageService
{
    private readonly IMessenger _messenger;

    public MessageService(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void SendActiveUser(IPerson user)
    {
        if (user != null)
        {
            _messenger.Send(new ActiveUserMessage() { User = user });
        }
    }

    public void SendProfilesUsers(MyDbContext myDbContext)
    {
       
        if (myDbContext != null) 
        {
            _messenger.Send(new AllProfilesMessage() { AllUsers = myDbContext.Users.ToList(), AllAdministrators = myDbContext.Administrators.ToList() }) ;
        
        }


    }

    public void SendVerifyCode(string VerifyCode)
    {
        if (VerifyCode != null)
        {
            _messenger.Send(new VerifyCodeMessage() { Code = VerifyCode});

        }
    }

    public void SentAllProducts(List<Products> products)
    {
        if(products != null) 
        {
            _messenger.Send(new AllProductsMessage() { Products = products });        
        }
    }

    public void SentAllStorages(List<Storage> storages)
    {
        if (storages != null)
        {
            _messenger.Send(new AllStoragesMessage() { storages = storages });
        }
    }

    public void SentEmailToReset(string Email)
    {
        if (Email != null) 
        {
            _messenger.Send(new EmailToResetMessage() { Email = Email });
        }
    }

    public void SentInfoToUserRegisterMessage(string Username, string Email, string Password)
    {
        if (Username != null && Email != null && Password != null)
        {
            _messenger.Send(new InfoToUserRegisterMessage() { UserName = Username, Email = Email, Password = Password  });

        }
    }

}
