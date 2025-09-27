using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private IServiceProvider _serviceProvider;

        public MainViewModel( IServiceProvider serviceProvider)
        {
          _serviceProvider = serviceProvider;
          
            CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
            
        }

        [ObservableProperty]
        private ObservableObject _currentViewModel = null!;


        [RelayCommand]
        private void NavigateToHome()
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
        }

        [RelayCommand]
        private void NavigateToAlert()
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<AlertViewModel>();
        }

        [RelayCommand]
        private void NavigateToFanStatus()
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<FanStatusViewModel>();
        }

        [RelayCommand]
        private void NavigateToControlFan()
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<CommandControlViewModel>();
        }


    }
}
