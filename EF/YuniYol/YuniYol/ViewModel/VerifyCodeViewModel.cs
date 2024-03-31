using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.ViewModel;

internal class VerifyCodeViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;

    public string VerifyCode { get; set; }
    public string ConfirmCode { get; set; }

    public string Userame { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }


    public VerifyCodeViewModel(INavigationService navigationService, IMessenger messenger, IMessageService messageService, IUserService userService)
    {
        _navigationService = navigationService;
        _messenger = messenger;
        _messageService = messageService;
        _userService = userService;

        _messenger.Register<VerifyCodeMessage>(this, message =>
        {
            ConfirmCode = message.Code;
        });

        _messenger.Register<InfoToUserRegisterMessage>(this, message =>
        {
            Userame = message.UserName;
            Email = message.Email;
            Password = message.Password;
        });

    }


    public RelayCommand VerifyBtn
    {
        get => new(
            () =>
            {
                if (VerifyCode == ConfirmCode)
                {

                    _userService.Register(Userame, Email, Password);
                    _navigationService.NavigateTo<ChoiceViewModel>();
                    VerifyCode = "";
                }

                else
                {
                    MessageBox.Show("Wrong Verify Code!!");
                }
            }




        );
    }


    public RelayCommand BackBtn
    {
        get => new(
        () =>
        {
            _navigationService.NavigateTo<RegisterViewModel>();
        }
        );
    }
}

