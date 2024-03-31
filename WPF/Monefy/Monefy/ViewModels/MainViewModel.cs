using GalaSoft.MvvmLight;
using Monefy.Services.Interfaces;
using Monefy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System.Windows.Navigation;
using INavigationService = Monefy.Services.Interfaces.INavigationService;
using Monefy.Messages;
using System.Windows;

namespace Monefy.ViewModels;

class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private ViewModelBase currentView;

    public ViewModelBase CurrentView
    {
        get => currentView;
        set => Set(ref currentView, value);
    }

    public MainViewModel(INavigationService navigationService, IMessenger messenger)
    {
        CurrentView = App.Container.GetInstance<BudgetViewModel>();

        _navigationService = navigationService;
        _messenger = messenger;

        _messenger.Register<NavigationMessage>(this, message =>
        {
            CurrentView = message.ViewModelType;
        });
    }

    
}
