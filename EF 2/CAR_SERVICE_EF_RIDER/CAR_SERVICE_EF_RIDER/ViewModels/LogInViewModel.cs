using System.ComponentModel;
using System.Windows;
using CAR_SERVICE_EF_RIDER.Interfaces;
using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using CAR_SERVICE_EF_RIDER.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Serilog;

namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class LogInViewModel : ViewModelBase 
{
    private readonly INavigationService NavigationService;
    private readonly IMessagesService MessagesService;
    private readonly ILogInService LogInService;
    private readonly ILoggerService LoggerService;
    private readonly MyDbContext DbContext = new MyDbContext();
    
    public string Email { get; set; }
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
    
    public LogInViewModel(INavigationService navigationService,IMessagesService messagesService,ILogInService logInService,ILoggerService loggerService)
    {
        NavigationService = navigationService;
        MessagesService = messagesService;
        LogInService = logInService;
        LoggerService = loggerService;
    }
    
    
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
    
    
    


    public RelayCommand DontHaveAccountCommand
    {
        
        get => new(
            
            () =>
            {
                NavigationService.NavigateTo<RegisterViewModel>();
                Email = string.Empty;
            });
    }

    public RelayCommand LogInCommand
    {
        get => new(
            () =>

            {
                if (LogInService.LogInUser(Email, Password) as User != null)
                {
                    
                    NavigationService.NavigateTo<UserMainViewModel>();
                    MessagesService.SendActiveUser(new User(DbContext.Users.Where(n => n.Email == Email).Select(u => u).ToList().First()));
                    LoggerService.CreateLog($"User '{DbContext.Users.Where(u => u.Email == Email).ToList().First().Name}' has logged into the account\n ",1);
                    Email = string.Empty;// Password не обнуляю так как тогда сбивается биндинг с passwordbox.Косяк который так и не смог решить
                    return;
                }
                
                if (LogInService.LogInAdmin(Email, Password) as Admin != null)
                {
                    NavigationService.NavigateTo<AdminMainViewModel>();
                    MessagesService.SendActiveUser(new Admin(DbContext.Admins.Where(A => A.Email == Email).Select(A => A).ToList().First()));
                    LoggerService.CreateLog($"Admin '{DbContext.Admins.Where(u => u.Email == Email).ToList().First().Name}' has logged into the account\n ",1);
                    Email = string.Empty;// Password не обнуляю так как тогда сбивается биндинг с passwordbox.Косяк который так и не смог решить
                    return;
                }

            });
    }

    public RelayCommand ForgotPasswordCommand
    {
        get => new(() =>
        {
            NavigationService.NavigateTo<ForgotPasswordViewModel>();
            Email = string.Empty;
        });
    }
    
}