using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using CAR_SERVICE_EF_RIDER.Messages;
using CAR_SERVICE_EF_RIDER.Models;
using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

namespace CAR_SERVICE_EF_RIDER.ViewModels;

public class CreateAdViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly INavigationService NavigationService;
    private readonly IMessagesService MessagesService;
    private readonly IMessenger Messenger;
    private readonly ICreateAdService CreateAdService;
    private readonly ILoggerService LoggerService;
    
    // Что бы следить за обновлением картинки
    private BitmapImage _selectedImage;
    public BitmapImage SelectedImage
    {
        get => _selectedImage;
        set
        {
            _selectedImage = value;
            OnPropertyChanged();
        }
    }
    
    public RelayCommand UploadImageCommand { get; }
    
    
    public string Title { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public byte[] ImageBytes { get; set; }
    
    public User ActiveUser { get; set; }

    public CreateAdViewModel(INavigationService navigationService,IMessagesService messagesService,IMessenger messenger,ICreateAdService createAdService,ILoggerService loggerService)
    {
        NavigationService = navigationService;
        MessagesService = messagesService;
        Messenger = messenger;
        CreateAdService = createAdService;
        LoggerService = loggerService;
        
        Messenger.Register<ActiveUserMessage>(this, message =>
        {
            ActiveUser = message.Person as User;
        });
        
        UploadImageCommand = new RelayCommand(() => UploadImage()); // уточнить!
      
    }


    private void UploadImage()
    {
        OpenFileDialog dialog = new OpenFileDialog
        {
            Filter = "Picture (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
            Title = "Select Picture"
        };

        if (dialog.ShowDialog() == true)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new System.Uri(dialog.FileName);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            SelectedImage = bitmap;
        }

        ImageBytes = File.ReadAllBytes(dialog.FileName);
    }
    
    
    public RelayCommand BackCommand
    {
        get => new(() =>
        {
            NavigationService.NavigateTo<UserMainViewModel>();
            Title = string.Empty;
            Description = string.Empty;
            Price = 0;
            Date = DateTime.Today;
            SelectedImage = null;
        });
    }
    
    
    public RelayCommand CreateAdCommand
    {
        get => new(() =>
        {
            CreateAdService.Create_Ad(Title,Price,Description,ImageBytes,ActiveUser);
            NavigationService.NavigateTo<UserMainViewModel>();
            MessagesService.UpdateInfoMessage();
            LoggerService.CreateLog($"User '{ActiveUser.Name}' created ad '{Title}'",1);
            Title = string.Empty;
            Description = string.Empty;
            Price = 0;
            Date = DateTime.Today;
            SelectedImage = null;
        });
    }
    
    
    public event PropertyChangedEventHandler PropertyChanged; // Что бы сообщать UI о том что данные изменились 
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}