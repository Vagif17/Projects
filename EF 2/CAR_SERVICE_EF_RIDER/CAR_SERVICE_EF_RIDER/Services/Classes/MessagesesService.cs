using CAR_SERVICE_EF_RIDER.Interfaces;
using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;

namespace CAR_SERVICE_EF_RIDER.Services.Classes;

public class MessagesesService : IMessagesService
{
    private readonly IMessenger Messenger; // Это для чего было?
    private IMessagesService _messagesServiceImplementation;

    public MessagesesService(IMessenger messenger)
    {
        Messenger = messenger;
    }

    public void SendActiveUser(IPerson user)
    {
        if (user != null)
        {
            Messenger.Send(new ActiveUserMessage() {Person = user});
        };
    }

    public void SendVerifyCode(string VerifyCode)
    {
        if (VerifyCode != null)
        {
            Messenger.Send(new VerifyCodeMessage() {VerifyCode = VerifyCode});
        }
    }

    public void SentRestoreEmail(string Email)
    {
        if (Email != null)
        {
            Messenger.Send(new EmailForRestoreMessage() {Email = Email} );
        }
    }

    public void UpdateInfoMessage()
    {
        Messenger.Send(new UpdateInfoMessage() {DbContext = new MyDbContext()});
    }
}