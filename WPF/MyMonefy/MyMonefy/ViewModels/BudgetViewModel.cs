using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using MyMonefy.Messages;
using MyMonefy.Models;
using MyMonefy.Services.Classes;
using MyMonefy.Services.Interfaces;
using MyMonefy.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace MyMonefy.ViewModels;

internal class BudgetViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private readonly IDataService _dataService;
    private readonly IBudgetService _budgetService; 

    private PieChart chart = new();
    public PieChart PieChart { get => chart; set => Set(ref chart); }

    private Expenses Expenses { get; set; }

    public object[] data = new object[3];
    public string ActiveUserName { get; set; }

    public BudgetViewModel(INavigationService navigationService, IMessenger messenger, IDataService dataService, IBudgetService budgetService)
    {
        _navigationService = navigationService;
        _messenger = messenger;
        _dataService = dataService;
        _budgetService = budgetService;


        PieChart.InnerRadius = 50;


        _messenger.Register<DataMessage>(this, message =>
        {

            if (message.Data[0] != null && message.Data[1] != null && message.Data[2] != null)
            {
                PieChart.Series.Add(
                               new PieSeries
                               {

                                   Values = new ChartValues<Double> { Convert.ToDouble(message.Data[2]) },
                                   Fill = (Brush)message.Data[1],
                                   Title = Convert.ToString(message.Data[0]),

                               }); ;
            }

        });

        _messenger.Register<ExpensesMessage>(this, message =>
        {

            if (message.Expenses != null)
            {

                _budgetService.ExpnesesSerialization(message.Expenses);

            }


        });

        _messenger.Register<ActiveUserMessage>(this, message => 
        {
            
            if (message.Name != null) 
            {
                ActiveUserName = message.Name; 
            }


        });

       
    }


    public RelayCommand<Button> addBtn
    {
        get => new(
            btn =>
            {
                data[0] = btn.Name; // Category
                data[1] = btn.Foreground;
                _dataService.SendData(data);
                _navigationService.NavigateTo<BudgetEnterViewModel2>(); // Должен был в BudgetEnterViewModel переходть.Он работает,но не отображает числа которые вы вводите во время ввода ,ну то есть не работает да.Короче говоря он на ремонте

            });
    }


    public RelayCommand logoutCommand
    {
        get => new(
            () =>
            {

                _navigationService.NavigateTo<ChoiceViewModel>();

            });
    }
}
