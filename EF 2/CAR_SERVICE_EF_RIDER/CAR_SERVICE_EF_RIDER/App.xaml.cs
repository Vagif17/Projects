using System.Configuration;
using System.Data;
using System.Windows;
using CAR_SERVICE_EF_RIDER.Services.Classes;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using CAR_SERVICE_EF_RIDER.ViewModels;
using CAR_SERVICE_EF_RIDER.Views;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CAR_SERVICE_EF_RIDER;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static SimpleInjector.Container Container { get; set; }

    void Register() //Уточнить для что-конкретно мы тут делали!!!!!
    {
        Container = new();

        Container.RegisterSingleton<INavigationService, NavigationService>(); //что здесь делает RegisterSingleton<>? 
        Container.RegisterSingleton<IMessenger,Messenger>(); // Это нужно было для работы NavigationSerice,но что именно Messenger делает?
        Container.RegisterSingleton<IMessagesService,MessagesesService>();
        Container.RegisterSingleton<IUserRegistrationService,UserRegistrationService>();
        Container.RegisterSingleton<ILogInService,LogInService>();
        Container.RegisterSingleton<IUserRestorePasswordService,UserRestorePasswordService>();
        Container.RegisterSingleton<ICreateAdService,CreateAdService>();
        Container.RegisterSingleton<ILoggerService,LoggerService>();
        
        
        Container.RegisterSingleton<MainWindowViewModel>(); // Тут RegisterSingleton<>() создаёт 1 usercontrol на всё время использование приложения?
        Container.RegisterSingleton<RegisterViewModel>();
        Container.RegisterSingleton<LogInViewModel>();
        Container.RegisterSingleton<ForgotPasswordViewModel>();
        Container.RegisterSingleton<VerifyCodeViewModel>();
        Container.RegisterSingleton<NewPasswordViewModel>();
        Container.RegisterSingleton<UserMainViewModel>();
        Container.RegisterSingleton<CreateAdViewModel>();
        Container.RegisterSingleton<AdminMainViewModel>();
        Container.RegisterSingleton<AdminUserControlViewModel>();
        Container.RegisterSingleton<AdminProductControlViewModel>();
        
        Container.Verify();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        Register(); // Что за регистер?

        var window = new MainWindowView();
        window.DataContext = Container.GetInstance<MainWindowViewModel>(); // Открывает usercontrol который щас нужен из котейнера?

        window.ShowDialog();
    }
}