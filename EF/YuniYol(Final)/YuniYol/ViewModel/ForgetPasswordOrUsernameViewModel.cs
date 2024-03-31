using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Models;
using YuniYol.Service.Classes;
using YuniYol.Service.Interfaces;

namespace YuniYol.ViewModel;

internal class ForgetPasswordOrUsernameViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessageService _messageService;
    private readonly IMessenger _messenger;
    private readonly IUserService _userService;

    public string Email { get; set; }
    public string VerifyCode { get; set; }
    public string ConfirmCode { get; set; } 
    public ForgetPasswordOrUsernameViewModel(INavigationService navigationService, IMessageService messageService, IMessenger messenger,IUserService userService)
    {
        
        _navigationService = navigationService;
        _messageService = messageService;
        _messenger = messenger;
        _userService = userService;




    }



    public RelayCommand BackBtn
    {
        get => new
           (

       () =>
       {
           _navigationService.NavigateTo<ChoiceViewModel>();
           Email = "";
       }

           );
    }


    public RelayCommand FindBtn
    {
        get => new(
            () =>
            {
                MyDbContext context = new MyDbContext();

                List<Users> users = context.Users.ToList();

                for (int i = 0; i < users.Count; i++) // Return profile can only Users because Administrators and Moderator must never forget their info.Why?Because. 
                {
                    if (Email == "" || Email == null)
                    {
                        MessageBox.Show("Enter Email!");
                        return;
                    }

                    if (Email == users[i].Email)
                    {
                        MessageBox.Show("Account finded!We sent verify code to you'r email");
                        ConfirmCode = _userService.SentEmail(Email);
                    }

                    else
                    {
                        MessageBox.Show("Account was not found!");
                        return;
                    }

                }

            });
            
    }

    public RelayCommand VerifyBtn
    {
        get => new(
            () =>
            {

                if (VerifyCode == "" || VerifyCode == null)
                {
                    MessageBox.Show("Enter Verify Code");
                    return;
                }

                if (VerifyCode == ConfirmCode)
                {
                    _messageService.SentEmailToReset(Email);
                    _navigationService.NavigateTo<ReturnAccountViewModel>();
                    
                }

                else
                {
                    MessageBox.Show("Wrong Verify Code!!");
                }
            }

        );
    }

}
