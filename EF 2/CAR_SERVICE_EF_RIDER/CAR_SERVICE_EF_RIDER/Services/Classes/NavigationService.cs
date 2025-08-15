using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace CAR_SERVICE_EF_RIDER.Services.Classes;

public class NavigationService : INavigationService
{
    private readonly IMessenger _messenger;


    public NavigationService(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void NavigateTo<T>() where T : ViewModelBase
    {
        _messenger.Send(new NavigationMessage()
        {
            ViewModelType = App.Container.GetInstance<T>()
        });
    }
    
   
}