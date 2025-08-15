using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;

namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class AdminMainViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly IMessagesService MessagesService;
    private readonly IMessenger Messenger;
    private readonly INavigationService NavigationService;
    private ViewModelBase currentView;

    public ViewModelBase CurrentView
    {

        get => currentView;
        set
        {
            currentView = value;
            OnPropertyChanged();
        }
        
    }
    

    public Admin Admin { get; set; }
    public ObservableCollection<User> Users { get; set; }
    public ObservableCollection<Item> Items { get; set; }

    public MyDbContext DbContext = new MyDbContext();

    public AdminMainViewModel(IMessagesService messagesService, IMessenger messenger,
        INavigationService navigationService)
    {
        MessagesService = messagesService;
        Messenger = messenger;
        NavigationService = navigationService;


        Messenger.Register<ActiveUserMessage>(this, message => { Admin = message.Person as Admin; });

        
        Users = new ObservableCollection<User>(); 
        Items = new ObservableCollection<Item>(DbContext.Items.ToList());



    }

    public RelayCommand UserCommand
    {
        get => new(() =>
        {
            CurrentView = App.Container.GetInstance<AdminUserControlViewModel>();
        });
    }

    public RelayCommand ProductCommand
    {
        get => new(() =>
        {
            CurrentView = App.Container.GetInstance<AdminProductControlViewModel>();
        });
    }
    
    
    public RelayCommand ExitCommand
    {
        get => new(() =>
        {
            NavigationService.NavigateTo<LogInViewModel>();
        });
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

}