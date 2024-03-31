using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMonefy.Services.Interfaces;
using System.Windows;
using MyMonefy.Models;
using System.IO;
using MyMonefy.Services.Classes;

namespace MyMonefy.ViewModels;


class RegistrationViewModel : ViewModelBase
{

    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private readonly IUserService _userService;
    private readonly IRegistrationParametrsService _registrationParametrsService;

    public Account Account { get; set; } = new Account();
    public string Confirmpassword { get; set; }

    public RegistrationViewModel(INavigationService navigationService, IMessenger messenger,IUserService userService, IRegistrationParametrsService registrationParametrsService)
    {
        _navigationService = navigationService;
        _messenger = messenger;
        _userService = userService;
        _registrationParametrsService = registrationParametrsService;
    }


    public MyRelayCommand submitBtn
    {
        get => new(
            () =>
            {
                _userService.Register(Account);
                _navigationService.NavigateTo<ChoiceViewModel>();
            },
            () =>
            {

                if (Account.Name != null && Account.Surname != null && Account.Email != null && Account.Password != null)
                {
                    return _registrationParametrsService.CheckAll(Account.Name, Account.Surname, Account.Email,Account.Password,Confirmpassword);
                }
                return false  ;

            }
            );
    }


    public MyRelayCommand gobackCommand
    {
        get => new(
        () =>
        {
            _navigationService.NavigateTo<ChoiceViewModel>();
        }
        );
    }
}
