using System.ComponentModel;
using System.Windows;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class RegisterViewModel : ViewModelBase, INotifyPropertyChanged // INotifyPropertyChanged для привязки PasswordBox с MVVM
{
    private readonly IUserRegistrationService UserRegistrationService;
    private readonly INavigationService NavigationService;
    private readonly IMessagesService MessagesService;
    private readonly ILoggerService LoggerService;
    private readonly MyDbContext DbContext = new MyDbContext();
    
    public RegisterViewModel(INavigationService navigationService,IUserRegistrationService registrationService,IMessagesService messagesService,ILoggerService loggerService)
    { 
        NavigationService = navigationService;
        UserRegistrationService = registrationService;
        MessagesService = messagesService;
        LoggerService = loggerService;
    }
    
    
    
    
    public string UserName { get; set; }
    public string Email { get; set; }
    
    //для привязки PasswordBox с MVVM
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
    
    

    public RelayCommand HaveAccountCommand
    {
        get => new(() =>
            {
                NavigationService.NavigateTo<LogInViewModel>();
                UserName = string.Empty;
                Email = string.Empty;
            }
        );

    }

    public RelayCommand RegisterCommand
    {
        get => new(() =>
            {
                if (UserRegistrationService.Check_Parameters(Email, UserName, Password, ConfirmPassword))
                {
                    UserRegistrationService.Register_User(Email,UserName,Password);
                    MessageBox.Show("You are successful registered!","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Information);
                    MessagesService.SendActiveUser(new User(DbContext.Users.Where(n => n.Email == Email).Select(u => u).ToList().First()));
                    MessagesService.UpdateInfoMessage();
                    LoggerService.CreateLog($"New user '{UserName} have registered' ",1);
                    NavigationService.NavigateTo<UserMainViewModel>();
                    UserName = string.Empty;
                    Email = string.Empty;
                };
            }
        );
    }
}