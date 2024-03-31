using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MyMonefy.Services.Classes;
using MyMonefy.Services.Interfaces;
using MyMonefy.ViewModels;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Xml.Schema;
using INavigationService = MyMonefy.Services.Interfaces.INavigationService;
using NavigationService = MyMonefy.Services.Classes.NavigationService;

namespace MyMonefy;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    public static SimpleInjector.Container Container { get; set; }

    void Register()
    {
        Container = new();

        Container.RegisterSingleton<IMessenger, Messenger>();
        Container.RegisterSingleton<INavigationService, NavigationService>();
        Container.RegisterSingleton<IUserService, UserService>();
        Container.RegisterSingleton<ICalculateService, CalculateService>();
        Container.RegisterSingleton<IDataService, DataService>();
        Container.RegisterSingleton<IRegistrationParametrsService, RegistrationParametrsService>();
        Container.RegisterSingleton<IBudgetService, BudgetService>();

        Container.RegisterSingleton<MainViewModel>();
        Container.RegisterSingleton<ChoiceViewModel>();
        Container.RegisterSingleton<RegistrationViewModel>();
        Container.RegisterSingleton<LogInViewModel>();
        Container.RegisterSingleton<BudgetViewModel>();
        Container.RegisterSingleton<BudgetEnterViewModel>();
        Container.RegisterSingleton<BudgetEnterViewModel2>();

        Container.Verify();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Register();

        var window = new MainWindow();
        window.DataContext = Container.GetInstance<MainViewModel>();

        window.ShowDialog();
    }


}
