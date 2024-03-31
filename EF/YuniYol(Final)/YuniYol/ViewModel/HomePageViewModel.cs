using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.ViewModel;

internal class HomePageViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private readonly IUserService _userService;
    private readonly IMessageService _messageService;

    public MyDbContext context = new MyDbContext();
    public Users User { get; set; }
    public ObservableCollection<Products> Products { get; set; } = new ObservableCollection<Products>();
    public List<int> ProductsId { get; set; } = new List<int>();

    public Products SelectedProduct { get; set; }
    public string SearchedTitle { get; set; }

    public HomePageViewModel(INavigationService navigationService, IMessenger messenger, IUserService userService, IMessageService messageService)
    {
        _navigationService = navigationService;
        _messenger = messenger;
        _userService = userService;
        _messageService = messageService;



        _messenger.Register<AllStoragesMessage>(this, message =>
        {
            Products.Clear();

            foreach (Storage item in message.storages)
                {
                    ProductsId.Add(item.ProductId);
                }

        });
           

        

        _messenger.Register<AllProductsMessage>(this, message => // For live list,to see what was created in runtime
        {



            if (ProductsId.Count > 0)

            {
                bool IsToAdd = false;

                foreach (Products item in message.Products)
                {
                    foreach (int productId in ProductsId)
                    {
                        if ( item.Id == productId)
                        {
                            IsToAdd = false;
                            break;
                        }

                        else
                        {
                            IsToAdd = true;
                        }
                        
                    }

                    if (IsToAdd)
                    {
                        Products.Add(item);
                    }


                }

            }

            else
            {
                foreach (Products item in message.Products)
                {

                  
                        Products.Add(item);
                    
                }
            }

            ProductsId.Clear();

        });

      

        _messenger.Register<ActiveUserMessage>(this, message =>
        {

            User = message.User as Users;

        });

        

    }

    public RelayCommand ExitBtn
    {
        get => new(
            () =>
            {
                _navigationService.NavigateTo<ChoiceViewModel>();
                SearchedTitle = string.Empty;

                Products.Clear();
               
            }
        );
    }

    public RelayCommand SearchBtn
    {
        get => new(
            () =>
            {

                List<Products> products = context.Products.ToList();

                Products.Clear();
                foreach (Products item in products)
                {
                    if (item.Name == SearchedTitle)
                    {
                        Products.Add(item);
                    }
                }


                if (SearchedTitle == null || SearchedTitle.Length == 0)
                {
                    MessageBox.Show("Not Found!");
                }
            }
        );
    }

    public RelayCommand ShowAllBtn
    {
        get => new(
            () =>
            {
                Products.Clear();
                foreach (Products item in context.Products)
                {

                    Products.Add(item);

                }
            }
        );
    }

    public RelayCommand BuyBtn
    {
        get => new(
            () =>
            {
                int UserId = context.Users.First(x => x.Name == User.Name).Id;
                int ProductId = SelectedProduct.Id;

                Storage storage = new Storage { ProductId = ProductId, UserId = UserId ,OrderDate = DateTime.Now,TrackinId = "Ordered" };
                context.Storages.Add(storage);
                context.SaveChanges();


                List<Storage> storages = context.Storages.ToList();
                _messageService.SentAllStorages(storages);

                Products.Clear();

                ProductsId.Clear();

                if (ProductsId.Count > 0)

                {
                    bool IsToAdd = false;

                    foreach (Products item in context.Products)
                    {
                        foreach (int productId in ProductsId)
                        {
                            if (item.Id == productId)
                            {
                                IsToAdd = false;
                                break;
                            }

                            else
                            {
                                IsToAdd = true;
                            }

                        }

                        if (IsToAdd)
                        {
                            Products.Add(item);
                        }


                    }

                }



            });
    }
}
