using GalaSoft.MvvmLight.Messaging;
using System.Configuration;
using System.Data;
using System.Windows;
using YuniYol.Service.Classes;
using YuniYol.Service.Interfaces;
using YuniYol.Services.Classes;
using YuniYol.ViewModel;
using YuniYol.Views;

namespace YuniYol;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static SimpleInjector.Container Container { get; set; }

    void Register()
    {
        Container = new();

        Container.RegisterSingleton<IMessageService, MessageService>();
        Container.RegisterSingleton<INavigationService, NavigationService>();
        Container.RegisterSingleton<IUserService, UserService>();
        Container.RegisterSingleton<IAdministratorsControlService, AdministratorsControlService>();
        Container.RegisterSingleton<IUsersControlService, UsersControlService>();
        Container.RegisterSingleton<IProductsControlService, ProductsControlService>();
        Container.RegisterSingleton<IMessenger,Messenger>();
        Container.RegisterSingleton<IStorageControlService, StorageControlService>();

        Container.RegisterSingleton<MainWindowViewModel>();
        Container.RegisterSingleton<HomePageViewModel>();
        Container.RegisterSingleton<ChoiceViewModel>();
        Container.RegisterSingleton<RegisterViewModel>();
        Container.RegisterSingleton<ModeratorViewModel>();
        Container.RegisterSingleton<AdministratorsControlViewModel>();
        Container.RegisterSingleton<UsersControlViewModel>();
        Container.RegisterSingleton<AdministratorViewModel>();
        Container.RegisterSingleton<VerifyCodeViewModel>();
        Container.RegisterSingleton<ForgetPasswordOrUsernameViewModel>();
        Container.RegisterSingleton<ReturnAccountViewModel>();
        Container.RegisterSingleton<ProductsControlViewModel>();
        Container.RegisterSingleton<StorageControlViewModel>();

        Container.Verify();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Register();

        var window = new MainWindow();
        window.DataContext = Container.GetInstance<MainWindowViewModel>();

        window.ShowDialog();
    }
}
