using System.ComponentModel;
using System.Windows;
using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using static BCrypt.Net.BCrypt; //??

namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class NewPasswordViewModel : ViewModelBase
{
    private readonly IUserRestorePasswordService RestorePasswordService;
    private readonly INavigationService NavigationService;
    private readonly IMessenger Messenger;
    private readonly ILoggerService LoggerService;
    public string Email { get; set; }
    
    public NewPasswordViewModel(IUserRestorePasswordService restorePasswordService,INavigationService navigationService,IMessenger messenger, ILoggerService loggerService)
    {
        RestorePasswordService = restorePasswordService;
        NavigationService = navigationService;
        Messenger = messenger;
        LoggerService = loggerService;
        
        Messenger.Register<EmailForRestoreMessage>(this, message =>
        {
            Email = message.Email;
        });
    }
    
    
    private string _password;
    
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    private string _confirmpassword;

    public string ConfirmPassword
    {
        get => _confirmpassword;
        set
        {
            _confirmpassword = value;
            OnPropertyChanged(nameof(ConfirmPassword));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }


    public RelayCommand ConfirmCommand
    {
        get => new(() =>
        {
            RestorePasswordService.Update_Database(Password,ConfirmPassword,Email);
            NavigationService.NavigateTo<LogInViewModel>();
            User user = new User();
            using (var DbContext = new MyDbContext())
            {
                user = DbContext.Users.Where(U => U.Email == Email).ToList().First();
            }
            
            LoggerService.CreateLog($"User '{user.Name}' changed password",1);
            Email = string.Empty;
        });
    }
}