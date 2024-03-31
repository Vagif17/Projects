using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MyMonefy.Messages;
using MyMonefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonefy.ViewModels;

class MainViewModel : ViewModelBase
{

    private readonly Services.Interfaces.INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private ViewModelBase currentView;

    public ViewModelBase CurrentView
    {
        get => currentView;
        set => Set(ref currentView, value);
    }

    public MainViewModel(Services.Interfaces.INavigationService navigationService, IMessenger messenger)
    {
        CurrentView = App.Container.GetInstance<ChoiceViewModel>(); 
        _navigationService = navigationService;
        _messenger = messenger;

        _messenger.Register<NavigationMessage>(this, message =>
        {
            CurrentView = message.ViewModelType;
        });
    }


}
