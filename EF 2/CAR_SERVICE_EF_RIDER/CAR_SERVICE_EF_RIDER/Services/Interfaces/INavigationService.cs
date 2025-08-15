using GalaSoft.MvvmLight;
namespace CAR_SERVICE_EF_RIDER.Services.Interfaces;

public interface INavigationService
{
    public void NavigateTo<T>() where T : ViewModelBase;

}