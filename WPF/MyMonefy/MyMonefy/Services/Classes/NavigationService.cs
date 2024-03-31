using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using MyMonefy.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMonefy.Services.Interfaces;

namespace MyMonefy.Services.Classes;

public class NavigationService : INavigationService
{
    private readonly IMessenger _messenger;

    public NavigationService(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void NavigateTo<T>() where T : ViewModelBase
    {
        _messenger.Send(new NavigationMessage()
        {
            ViewModelType = App.Container.GetInstance<T>()
        });


    }
}

