using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class UserMainViewModel : ViewModelBase
{
    
    private readonly INavigationService NavigationService;
    private readonly IMessagesService MessagesService;
    private readonly IMessenger Messenger;

    
    public User ActiveUser { get; set; }
    
    public ObservableCollection<Item> Items { get; set; }
    
    
    public UserMainViewModel(INavigationService navigationService,IMessagesService messagesService,IMessenger messenger)
    {
        NavigationService = navigationService;
        MessagesService = messagesService;
        Messenger = messenger;

        
        Messenger.Register<ActiveUserMessage>(this, message =>
        {
            ActiveUser = message.Person as User;
        });
        
        
        using (var context = new MyDbContext())
        {
            Items = new ObservableCollection<Item>(context.Items.ToList());
        }
        
        Messenger.Register<UpdateInfoMessage>(this, message =>
        {
            Items = new ObservableCollection<Item>(message.DbContext.Items.ToList()); // для обновления списка во время рантайма
        });
        
    }

    public RelayCommand CreateAdCommand
    {
        get => new(() =>
        {
            NavigationService.NavigateTo<CreateAdViewModel>();
        });
    }

    public RelayCommand ExitCommand
    {
        get => new(() =>
        {
            NavigationService.NavigateTo<LogInViewModel>();
        });
    }
}