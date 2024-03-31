using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MyMonefy.Services.Classes;
using MyMonefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using INavigationService = MyMonefy.Services.Interfaces.INavigationService;

namespace MyMonefy.ViewModels;

class BudgetEnterViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;
    private readonly ICalculateService _calculateService;

    private string example = "";
    public string Example { get => example; set => Set(ref example); }

    public BudgetEnterViewModel(IMessenger messenger, Services.Interfaces.INavigationService navigationService,ICalculateService calculateService)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _calculateService = calculateService;
    }




    public RelayCommand<Button> addBtn
    {
        get => new(
            btn =>
            {
                example += _calculateService.AddToExample(btn.Name);

            });
    }

        public MyRelayCommand gobackCommand
    {
        get => new(
            () =>
            {
                
                _navigationService.NavigateTo<BudgetViewModel>();

            });
    }
}
