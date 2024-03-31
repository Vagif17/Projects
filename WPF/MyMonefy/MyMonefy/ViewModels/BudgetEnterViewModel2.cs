using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyMonefy.Messages;
using MyMonefy.Models;
using MyMonefy.Services.Classes;
using MyMonefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyMonefy.ViewModels;

class BudgetEnterViewModel2 : ViewModelBase 
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private readonly IDataService _dataService;

    public string Category { get; set; }
    public Brush Color { get; set; }
    public string ExpenseValue { get; set; }

    public Expenses Expenses { get; set; } = new Expenses();

    public Object[] Data { get; set; } = new Object[3];

    public BudgetEnterViewModel2(INavigationService navigationService, IMessenger messenger,IDataService dataService)
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

    public MyRelayCommand addcommand
    {
        get => new(
            () =>
            {
                Data[2] = ExpenseValue;
                Data[1] = Color;
                Data[0] = Category;

                Expenses.Value = Convert.ToInt32 (ExpenseValue);
                Expenses.Category = Category;

                _dataService.SendData(Data);

                _dataService.SendExpenses(Expenses);

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