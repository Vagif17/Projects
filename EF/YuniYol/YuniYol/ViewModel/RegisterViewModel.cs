using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.ViewModel;

internal class RegisterViewModel : ViewModelBase
{

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } 
    public string ConfirmPassword { get; set; } 



    private readonly INavigationService _navigationService;
    private readonly IMessageService _message;
    private readonly IUserService _userService;
    public RegisterViewModel(INavigationService navigationService, IMessageService message, IUserService userService)
    {
        _navigationService = navigationService;
        _message = message;
        _userService = userService;
    }

    public RelayCommand BackBtn
    {
        get => new
           (

       () =>
       {
           _navigationService.NavigateTo<ChoiceViewModel>();

           Username = string.Empty;
           Email = string.Empty;
           Password = string.Empty;
           ConfirmPassword = string.Empty;
       }

           );
    }

    public RelayCommand RegisterBtn
    {
        get => new
            (
            () =>
            {

                if (_userService.CheckParametrs(Username, Email, Password, ConfirmPassword))
                {

                    _message.SendVerifyCode(_userService.SentEmail(Email));
                    _message.SentInfoToUserRegisterMessage(Username, Email, Password);
                    _navigationService.NavigateTo<VerifyCodeViewModel>();

                    Username = string.Empty;
                    Email = string.Empty;
                    Password = string.Empty;
                    ConfirmPassword = string.Empty;

                }
            }

            );
    }
}
