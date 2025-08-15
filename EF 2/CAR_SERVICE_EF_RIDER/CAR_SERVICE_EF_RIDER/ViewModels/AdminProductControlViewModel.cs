using System.Collections.ObjectModel;
using System.Windows.Input;
using CAR_SERVICE_EF_RIDER.Interfaces;
using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class AdminProductControlViewModel : ViewModelBase
{
    private readonly INavigationService NavigationService;
    private readonly IMessagesService MessagesService;
    private readonly IMessenger Messenger;
    private readonly ILoggerService LoggerService;
    private MyDbContext DbContext { get; set; } = new MyDbContext();
    public ICommand DeleteCommand { get; }
    public ObservableCollection<Item> Items { get; set; }
    private Admin ActiveAdmin { get; set; }

    public AdminProductControlViewModel(INavigationService navigationService, IMessenger messenger, IMessagesService messagesService,ILoggerService loggerService)
    {
        NavigationService = navigationService;
        MessagesService = messagesService;
        Messenger = messenger;
        LoggerService = loggerService;

        Items = new ObservableCollection<Item>(DbContext.Items.ToList());
        
        Messenger.Register<UpdateInfoMessage>(this, message =>
        {
            Items = new ObservableCollection<Item>(DbContext.Items.ToList());
        });
        
        
        Messenger.Register<ActiveUserMessage>(this, message =>
        {
            ActiveAdmin = message.Person as Admin;
        });

        DeleteCommand = new RelayCommand<Item>(DeleteItem);

    }
    

    private void DeleteItem(object obj)
    {
        if (obj is Item item && Items.Contains(item)) // ChatGPT Helped
        {
            Items.Remove(item);
            DbContext.Remove(item);
            DbContext.SaveChanges();
            MessagesService.UpdateInfoMessage();
            LoggerService.CreateLog($"Admin '{ActiveAdmin.Name}' deleted item '{item.Title}'",1);
        }
    }
    
}