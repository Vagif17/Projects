using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.ViewModel;
internal class AdministratorViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessageService _messageService;
    private readonly IMessenger _messenger;

    public Administrators Administrator { get;  set; } = new Administrators();

    public AdministratorViewModel(INavigationService navigationService,IMessageService messageService,IMessenger messenger)
    {
        _navigationService = navigationService;
        _messageService = messageService;
        _messenger = messenger;

        _messenger.Register<ActiveUserMessage>(this, message =>
        {
            Administrator = message.User as Administrators;
        });    

    }


    public RelayCommand BackBtn
    {
        get => new(
            () =>
            {
                _navigationService.NavigateTo<ChoiceViewModel>();
            }
        );
    }


    public RelayCommand UsersBtn
    {
        get => new(
            () =>
            {
                _navigationService.NavigateTo<UsersControlViewModel>();
            }
        );
    }
    public RelayCommand ProductsBtn
    {
        get => new(
            () =>
            {
                _navigationService.NavigateTo<ProductsControlViewModel>();
            }
        );
    }


    public RelayCommand StorageBtn
    {
        get => new(
            () =>
            {
                _navigationService.NavigateTo<StorageControlViewModel>();
            }
        );
    }

}