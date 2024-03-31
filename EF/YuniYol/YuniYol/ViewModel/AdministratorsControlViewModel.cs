using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Classes;
using YuniYol.Service.Interfaces;

namespace YuniYol.ViewModel;

class AdministratorsControlViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessageService _messageService;
    private readonly IMessenger _messenger;
    private readonly IAdministratorsControlService _administratorsControlService;

    public MyDbContext context = new MyDbContext();

    public ObservableCollection<Administrators> AdministratorsList { get; set; } = new ObservableCollection<Administrators>();
    public Administrators SelectedAdministrator { get; set; } = new Administrators();

    public AdministratorsControlViewModel(INavigationService navigationService, IMessageService messageService, IMessenger messenger, IAdministratorsControlService administratorsControlService)
    {
        _navigationService = navigationService;
        _messageService = messageService;
        _messenger = messenger;
        _administratorsControlService = administratorsControlService;

        _messenger.Register<AllProfilesMessage>(this, message =>
        {
            AdministratorsList.Clear();
            foreach (Administrators item in message.AllAdministrators)
            {
                AdministratorsList.Add(item);
            }
        });
    }


    public RelayCommand ExitBtn
    {
        get => new(

            () =>
            {

                _navigationService.NavigateTo<ModeratorViewModel>();


            }
       );
    }


    public RelayCommand DeleteBtn
    {
        get => new(
            () =>
            {
                _administratorsControlService.DeleteAdministrator(SelectedAdministrator, context);


                    AdministratorsList.Clear();
                foreach (Administrators item in context.Administrators) // for list live update
                {
                    AdministratorsList.Add(item);
                }


            }
      );
    }



    public RelayCommand MakeUserBtn
    {
        get => new(
            () =>
            {

                _administratorsControlService.MakeUser(SelectedAdministrator, context);



                AdministratorsList.Clear();
                foreach (Administrators item in context.Administrators) // for list live update
                {
                    AdministratorsList.Add(item);
                }

                _messageService.SendProfilesUsers(context);


            }

            );
    }
}
