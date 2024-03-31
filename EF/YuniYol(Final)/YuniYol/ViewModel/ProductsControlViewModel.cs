using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Interfaces;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.ViewModel;

internal class ProductsControlViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessageService _messageService;
    private readonly IMessenger _messenger;
    private readonly IProductsControlService _productsControlService;
    
    private IPerson _activeperson;

    public string Title { get; set; } = new("");
    public string Description { get; set; }
    public float Price { get; set; }

    public DateTime DateOfIssue { get; set; } = DateTime.Now;
    public DateTime ExpirationDate { get; set; } = DateTime.Now;
    
    
    MyDbContext myDbContext = new MyDbContext();

    public ObservableCollection<Products> Products { get; set; } = new  ObservableCollection<Products>();
    public Products SelectedProduct { get; set; }
    public List<Storage> Storages { get; set; }


    public List<int> ProductsId { get; set; } = new List<int>();
    public ProductsControlViewModel(INavigationService navigationService,IMessenger messenger,IMessageService messageService,IProductsControlService productsControlService)
    {
        _navigationService = navigationService;
        _messageService = messageService;
        _messenger = messenger;
        _productsControlService = productsControlService;   

        foreach (Products item in myDbContext.Products)
        {
            Products.Add(item);
        }

        _messenger.Register<ActiveUserMessage>(this, message =>
        {
            _activeperson = message.User;
        });

        _messenger.Register<AllStoragesMessage>(this, message =>
        {

            foreach (Storage item in message.storages)
            {
                ProductsId.Add(item.ProductId);
            }

        });

    }


    public RelayCommand BackBtn
    {
        get => new(
            () =>
            {
   
                if (_activeperson as Administrators != null) _navigationService.NavigateTo<AdministratorViewModel>();
                if (_activeperson as Moderators != null) _navigationService.NavigateTo<ModeratorViewModel>();
            }
        );
    }

    public RelayCommand CreateBtn
    {
        get => new(
            () =>
            {
                _productsControlService.CreateProduct(Price, Title, Description, ExpirationDate, DateOfIssue);



                 Products.Clear();
                foreach (Products item in myDbContext.Products)
                {
                    Products.Add(item);
                }

                List<Products> products = myDbContext.Products.ToList();
                _messageService.SentAllProducts(products);


            }
        );
    }

    public RelayCommand DeleteBtn
    {
        get => new(
            () =>
            {

                for (int i = 0; i < ProductsId.Count; i++)
                {
                    if (SelectedProduct.Id == ProductsId[i])
                    {
                        MessageBox.Show("This product already sold!");
                        return;
                    }
                }

                _productsControlService.DeleteProduct(SelectedProduct, myDbContext);

                Products.Clear();
                foreach (Products item in myDbContext.Products) // for list live update
                {
                    Products.Add(item);
                }


            }
      );
    }
}
