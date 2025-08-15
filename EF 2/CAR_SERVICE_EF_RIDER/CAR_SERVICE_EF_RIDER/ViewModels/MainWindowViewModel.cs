using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Serilog;


namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService NavigationService;
    private readonly IMessenger Messenger;
    private readonly ILoggerService LoggerService;
    private ViewModelBase currentView;
    
    public ViewModelBase CurrentView
    {
        
        get => currentView;
        set => Set(ref currentView, value); // Не совсем понял
        
    }

    public MainWindowViewModel(INavigationService navigationService, IMessenger messenger,ILoggerService loggerService)
    {
        
        CurrentView = App.Container.GetInstance<LogInViewModel>();
        NavigationService = navigationService;
        Messenger = messenger;
        LoggerService = loggerService;

        LoggerService.CreateLog("App Started",1);
        Messenger.Register<NavigationMessage>(this, message =>
        {
            CurrentView = message.ViewModelType;
        });
    }
}