using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Monefy.Messages;
using Monefy.Services;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using INavigationService = Monefy.Services.Interfaces.INavigationService;

namespace Monefy.ViewModels;

class BudgetEnterViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private readonly IDataService _dataService;

    public string Category { get; set; }
    public Brush Color { get; set; }
    public string ExpenseValue { get; set; }

    public Object[] Data { get; set; } = new Object[3];


    public BudgetEnterViewModel(INavigationService navigationService, IMessenger messenger, IDataService dataService) 
    {
        _navigationService = navigationService;
        _messenger = messenger;
        _dataService = dataService;


        _messenger.Register<DataMessage>(this, message =>
        {

            Category = message.Data[0].ToString();
            Color = (Brush)message.Data[1];

        });


    }


    public RelayCommand goback
    {
        
        get => new(
            () =>
            {
                _navigationService.NavigateTo<BudgetViewModel>();
            });
    }
    public MyRelayCommand addcommand
    {
        get => new(
            () =>
            {
                Data[2] = ExpenseValue;
                Data[1] = Color;
                Data[0] = Category;
                _dataService.SendData(Data);

                _navigationService.NavigateTo<BudgetViewModel>();
                ExpenseValue = "";

            },

            () =>
            {
                if (!String.IsNullOrEmpty(ExpenseValue)) 
                {
                    for (int i = 0; i < ExpenseValue.Length; i++)
                    {
                        if (ExpenseValue[i] < 48 || ExpenseValue[i] > 57)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            );                                                    
       

    }
    
}