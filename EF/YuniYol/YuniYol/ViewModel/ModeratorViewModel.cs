using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Interfaces;
using INavigationService = YuniYol.Service.Interfaces.INavigationService;

namespace YuniYol.ViewModel;
class ModeratorViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;

    public Moderators Moderator { get; set; }
    

    public ModeratorViewModel(INavigationService navigationService, IMessenger messenger)
    {
        _navigationService = navigationService;
        _messenger = messenger;

        _messenger.Register<ActiveUserMessage>(this, message =>
        {
            Moderator = message.User as Moderators;            
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

    public RelayCommand AdministratorsBtn
    {
        get => new(
            () =>
            {
                _navigationService.NavigateTo<AdministratorsControlViewModel>();
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
            });
    }

}
