using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Interfaces;
using YuniYol.Messages;
using YuniYol.Models;
using YuniYol.Service.Interfaces;
using Storage = YuniYol.Models.Storage;

namespace YuniYol.ViewModel;

internal class StorageControlViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessageService _messageService;
    private readonly IMessenger _messenger;
    private readonly IStorageControlService _storageControlService;
    private IPerson _activeperson;

    public MyDbContext DbContext { get; set; } = new MyDbContext();
    public ObservableCollectionListSource<Storage> StorageList { get; set; } = new ObservableCollectionListSource<Storage>();
    public Storage SelectedStorage { get; set; }

    public StorageControlViewModel(INavigationService navigationService, IMessageService messageService, IMessenger messenger, IStorageControlService storageControlService)
    {
        _navigationService = navigationService;
        _messageService = messageService;
        _messenger = messenger;
        _storageControlService = storageControlService;


        _messenger.Register<AllStoragesMessage>(this, message =>
        {
            StorageList.Clear();
            foreach (Storage item in message.storages)
            {
                StorageList.Add(item);
            }
        });
    }


    public RelayCommand BackBtn
    {
        get => new(
        () =>
        {
            _navigationService.NavigateTo<ModeratorViewModel>();

        });
    }



    public RelayCommand DeleteBtn
    {
        get => new(
            () =>
            {
                _storageControlService.DeleteStorage(SelectedStorage, DbContext);
                StorageList.Clear();

                foreach (Storage item in DbContext.Storages)
                {
                    StorageList.Add(item);
                }

            });
    }



    public RelayCommand Ordered
    {
        get => new(() =>
    {

        _storageControlService.OrderedFunc(SelectedStorage, DbContext);

        StorageList.Clear();

        foreach (Storage item in DbContext.Storages)
        {
            StorageList.Add(item);
        }
    });

    }
  



    public RelayCommand InTheStorage
    {
        get =>
            new(() =>
            {
                

               _storageControlService.InTheStorageFunc(SelectedStorage, DbContext);

                StorageList.Clear();
                foreach (Storage item in DbContext.Storages)
                {
                    StorageList.Add(item);
                }
            });
    }



    public RelayCommand Sended
    {
        get => new(() =>
        {
            _storageControlService.SendedFunc(SelectedStorage, DbContext);

            StorageList.Clear();

            foreach (Storage item in DbContext.Storages)
            {
                StorageList.Add(item);
            }

        });

    }

    public RelayCommand OnTheBorder
    {
        get => new(() =>
        {

            _storageControlService.OnTheBorderFunc(SelectedStorage, DbContext);
           
            StorageList.Clear();

            foreach (Storage item in DbContext.Storages)
            {
                StorageList.Add(item);
            }

        });

    }



    public RelayCommand AtThePostOffice
    {
        get => new(() =>
        {
            _storageControlService.AtThePostOffice(SelectedStorage, DbContext);

            StorageList.Clear();

            foreach (Storage item in DbContext.Storages)
            {
                StorageList.Add(item);
            }
        });


    }
}