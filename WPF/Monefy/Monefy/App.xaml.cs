using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Monefy.ViewModels;
using Monefy.Views;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using INavigationService = Monefy.Services.Interfaces.INavigationService;
using NavigationService = Monefy.Services.Classes.NavigationService;

namespace Monefy;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    public static Container Container { get; set; }

    void Register()
    {
        Container = new();

        Container.RegisterSingleton<IMessenger, Messenger>();
        Container.RegisterSingleton<INavigationService, NavigationService>();
        Container.RegisterSingleton<IDataService, DataService>();


        Container.RegisterSingleton<BudgetViewModel>();
        Container.RegisterSingleton<MainViewModel>();
        Container.RegisterSingleton<BudgetEnterViewModel>();

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
