using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Models;
using YuniYol.Service.Interfaces;
using INavigationService = YuniYol.Service.Interfaces.INavigationService;
namespace YuniYol.ViewModel;

internal class ChoiceViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessageService _message;
    private readonly IUserService _userService;

    MyDbContext MyDbContext { get; set; } = new MyDbContext();

    public string Username { get; set; }
    public string Password { get; set; }
    public ChoiceViewModel(INavigationService navigationService,IMessageService messenger, IUserService userService)
    {
        _navigationService = navigationService;
        _message = messenger;
        _userService = userService;

       
        
    }

    public RelayCommand RegistrationBtn
    {
        get => new
            (

        () =>
        {

            _navigationService.NavigateTo<RegisterViewModel>();
            Username = string.Empty;
            Password = string.Empty;
        }

            );
    }

    public RelayCommand LoginBtn
    {
        get => new
            (

        () =>
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) 
            {
                MessageBox.Show("Write Username or Password!");
                return;
            }


            



            if (_userService.Login(Username, Password) != null)
            {
                var User = _userService.Login(Username, Password);

                

                if (User as Users != null)
                {

                    _navigationService.NavigateTo<HomePageViewModel>();
                    var person = _userService.Login(Username, Password);
                    List<Products> products = MyDbContext.Products.ToList();
                    List<Storage> storages = MyDbContext.Storages.ToList();
                    _message.SentAllStorages(storages);
                    _message.SentAllProducts(products);
                    _message.SendActiveUser(person);
                }

                if (User as Moderators != null)
                {
                    _navigationService.NavigateTo<ModeratorViewModel>();
                    var person = _userService.Login(Username, Password);
                    List<Storage> storages = MyDbContext.Storages.ToList();
                    _message.SentAllStorages(storages);
                    _message.SendActiveUser(person);
                    _message.SendProfilesUsers(MyDbContext);
                }

                if (User as Administrators != null)
                {
                    _navigationService.NavigateTo<AdministratorViewModel>();
                    var person = _userService.Login(Username, Password);
                    List<Storage> storages = MyDbContext.Storages.ToList();
                    _message.SentAllStorages(storages);
                    _message.SendActiveUser(person);
                    _message.SendProfilesUsers(MyDbContext);
                }




                Username = string.Empty;
                Password = string.Empty;
            }

            

        }

            );
    }


    public RelayCommand ForgotPasswordOrUsernameBtn
    {
        get => new(
            () =>
            {
                
                _navigationService.NavigateTo<ForgetPasswordOrUsernameViewModel>();


            }
            );
    }

}
