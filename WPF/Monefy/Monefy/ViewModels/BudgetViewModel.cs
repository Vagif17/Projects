using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monefy.ViewModels;

class BudgetViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;    
    private readonly IDataService _dataService;


    private PieChart chart = new();
    public Double Income { get; set; }
    public PieChart Chart { get => chart; set => Set(ref chart); }

    public object[] data = new object[3];


    public BudgetViewModel(INavigationService navigationService, IMessenger messenger, IDataService dataService)
    {
        _navigationService = navigationService;
        _messenger = messenger;
        _dataService = dataService;

        Chart.InnerRadius = 40;


        _messenger.Register<DataMessage>(this, message =>
           {


               if (Convert.ToString (message.Data[0]) != "Plus" && message.Data[0] != null && message.Data[1] != null && message.Data[0] != null)
               {
                   Chart.Series.Add(
                                  new PieSeries
                                  {

                                      Values = new ChartValues<Double> { Convert.ToDouble(message.Data[2]) },
                                      Fill = (Brush)message.Data[1],
                                      Title = Convert.ToString(message.Data[0]),

                                  }); ;
               }


               if (Convert.ToString(data[0]) == "Plus" && message.Data[0] != null && message.Data[1] != null && message.Data[0] != null)
               {
                   Income += Convert.ToDouble(message.Data[2]);
               }


           });


    }

    public RelayCommand<Button> addBtn
    {
        get => new (
            btn => 
            {
                data[0] = btn.Name; // Category
                data[1] = btn.Foreground;
                if (btn.Name == "Minus") { data[1] = btn.Background; }
                _dataService.SendData(data);
                _navigationService.NavigateTo<BudgetEnterViewModel>();
            
            });
    }

   

}