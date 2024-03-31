using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Messages;
using YuniYol.Views;
using YuniYol.Service.Interfaces;

namespace YuniYol.ViewModel
{

    class MainWindowViewModel : ViewModelBase
    {

        private readonly INavigationService _navigationService;
        private readonly IMessenger _messenger;
        private ViewModelBase currentView;

        public ViewModelBase CurrentView
        {
            get => currentView;
            set => Set(ref currentView, value);
        }

        public MainWindowViewModel(INavigationService navigationService, IMessenger messenger)
        {
            CurrentView = App.Container.GetInstance<ChoiceViewModel>();
            _navigationService = navigationService;
            _messenger = messenger;

            _messenger.Register<NavigationMessage>(this, message =>
            {
                CurrentView = message.ViewModelType;
            });
        }


    }

}
