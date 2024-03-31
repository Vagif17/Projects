using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using MyMonefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyMonefy.ViewModels;

class ChoiceViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;

    private PieChart chart = new();
    public PieChart Chart { get => chart; set => Set(ref chart); }

    public ChoiceViewModel(INavigationService navigationService,IMessenger messenger)
    {
        _navigationService = navigationService;
        _messenger = messenger;


        for (int i = 0; i < 10; i++)
        {
            Chart.Series.Add(
                new PieSeries
                {

                    Values = new ChartValues<Double> { Convert.ToDouble(5) },

                }); 
                
        }

        Chart.InnerRadius = 50;

    }

    public RelayCommand registrationBtn
    {
        get => new
            (

        () =>
        {
            _navigationService.NavigateTo<RegistrationViewModel>();
        }

            );
    }

    public RelayCommand loginBtn
    {
        get => new
            (

        () =>
        {
            _navigationService.NavigateTo<LogInViewModel>();
        }

            );
    }

}
