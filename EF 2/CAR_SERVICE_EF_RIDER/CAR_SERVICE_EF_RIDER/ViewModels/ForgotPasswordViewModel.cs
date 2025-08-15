using System.Windows;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using CAR_SERVICE_EF_RIDER.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class ForgotPasswordViewModel : ViewModelBase
{
    private readonly INavigationService NavigationService;
    private readonly IMessagesService MessagesService;
    private readonly IUserRestorePasswordService UserRestorePasswordService;
    private readonly ILoggerService LoggerService;
    private readonly MyDbContext DbContext = new ();
    public string Email { get; set; }
    
    public ForgotPasswordViewModel(INavigationService navigationService,IMessagesService messagesService,IUserRestorePasswordService userRestorePasswordService,ILoggerService loggerService)
    {
        NavigationService = navigationService;
        MessagesService = messagesService;
        UserRestorePasswordService = userRestorePasswordService;
        LoggerService = loggerService;
    }


    public RelayCommand DontHaveAccountCommand
    {
        get => new(() =>
        {
            
            NavigationService.NavigateTo<RegisterViewModel>();

        });
    }

    
   
    
    public RelayCommand SendCommand
    {
        get => new(() =>
        {
            if (UserRestorePasswordService.Check_Parameters(Email))
            {
                if (UserRestorePasswordService.Find_User(Email))
                {
                    
                    MessagesService.SendVerifyCode(UserRestorePasswordService.Send_VerifyCode(Email)); 
                    MessagesService.SentRestoreEmail(Email);
                    NavigationService.NavigateTo<VerifyCodeViewModel>();
                    LoggerService.CreateLog($"Verify code was sent to user '{DbContext.Users.Where(u => u.Email == Email).ToList().First().Name}'",1);
                    Email = string.Empty;

                }
            }
        });
    }
}


