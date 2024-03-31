using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.TextFormatting;
using YuniYol.Interfaces;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.ViewModel;

class UsersControlViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private readonly IMessageService _messageService;
    private readonly IUsersControlService _usersControlService;

    private IPerson _activeperson;

    public MyDbContext context = new MyDbContext();

    public ObservableCollection<Users> UsersList { get; set; } = new ObservableCollection<Users>();
    public Users SelectedUser { get; set; } = new Users();

    public UsersControlViewModel(INavigationService navigationService, IMessenger messenger, IMessageService messageService, IUsersControlService usersControlService)
    {
        _navigationService = navigationService;
        _messenger = messenger;
        _messageService = messageService;
        _usersControlService = usersControlService;

        _messenger.Register<AllProfilesMessage>(this, message =>
        {
            UsersList.Clear();
            foreach (Users item in message.AllUsers)
            {
                UsersList.Add(item);
            }
        }
        );

        _messenger.Register<ActiveUserMessage>(this, message =>
        {

            _activeperson = message.User;


        });
    }


    public RelayCommand BackBtn
    {
        get => new(

            () =>
            {
                if (_activeperson as Moderators != null)  _navigationService.NavigateTo<ModeratorViewModel>();
                if (_activeperson as Administrators != null) _navigationService.NavigateTo<AdministratorViewModel>();


            }
       );
    }


    public RelayCommand DeleteBtn
    {
        get => new(
            () =>
            {
                _usersControlService.DeleteUser(SelectedUser, context);



                UsersList.Clear();
                foreach (Users item in context.Users) // for list live update
                {
                    UsersList.Add(item);
                }


            }
      );
    }



    public RelayCommand MakeAdministratorBtn
    {
        get => new(
            () =>
            {
                _usersControlService.MakeAdministrator(SelectedUser, context);


                UsersList.Clear();
                foreach (Users item in context.Users) // for list live update
                {
                    UsersList.Add(item);
                }

                _messageService.SendProfilesUsers(context);

            },
            () =>
            {
             

                if (_activeperson as Moderators != null)
                {
                    return true;
                }

                return false;
            }

            );
            
        
    }

}
