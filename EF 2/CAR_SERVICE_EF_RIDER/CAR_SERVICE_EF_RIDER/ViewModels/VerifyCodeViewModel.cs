using System.Windows;
using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class VerifyCodeViewModel : ViewModelBase
{
    private readonly INavigationService NavigationService;
    private readonly IMessenger Messenger;
    private readonly IMessagesService  MessagesService;
    public string VerifyCode { get; set; } = new("1");
    public string UsersCode { get; set; } 
        
    public VerifyCodeViewModel(INavigationService navigationService,IMessenger messenger,IMessagesService messagesService)
    {
        NavigationService = navigationService;
        Messenger = messenger;
        MessagesService = messagesService;
        
        Messenger.Register<VerifyCodeMessage>(this, message =>
        {
            VerifyCode = message.VerifyCode;
        });
    }

    public RelayCommand VerifyCommand
    {
        get => (new(() =>
                {
                    if (UsersCode == VerifyCode)
                    {
                        NavigationService.NavigateTo<NewPasswordViewModel>();
                        UsersCode = string.Empty;
                        VerifyCode = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect verify code!","ApexAuto",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                })
            );
    }

}