using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Interfaces;
using INavigationService = YuniYol.Service.Interfaces.INavigationService;
using static BCrypt.Net.BCrypt;

namespace YuniYol.ViewModel;

internal class ReturnAccountViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessageService _messageService;
    private readonly IMessenger _messenger;
    private readonly IUserService _userService;

    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public ReturnAccountViewModel(INavigationService navigationService, IMessageService messageService, IMessenger messenger, IUserService userService)
    {
        _navigationService = navigationService;
        _messageService = messageService;
        _messenger = messenger;
        _userService = userService;

        _messenger.Register<EmailToResetMessage>(this, message =>
        {
            Email = message.Email;
        });
    }


    public RelayCommand ConfirmBtn
    {
        get => new(
        () =>
        {
            if (Password == ConfirmPassword)
            {
                MyDbContext context = new MyDbContext();
                Users UserTmp = context.Users.First(x => x.Email == Email);
                
                context.Users.Remove(UserTmp);
                context.SaveChanges();

                UserTmp.Password = HashPassword(Password);
                UserTmp.Id = 0;

                context.Users.Add(UserTmp);
                context.SaveChanges();


                _navigationService.NavigateTo<ChoiceViewModel>();
            }

            else
            {
                MessageBox.Show("Passwords are different!");
            }

        });
    }


}