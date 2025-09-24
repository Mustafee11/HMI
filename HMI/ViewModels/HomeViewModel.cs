using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMI.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        public HomeViewModel(IServiceProvider serviceProvider)
        {
           _serviceProvider = serviceProvider;
        }

        [ObservableProperty]
        private string _title = "Översikt";

        [RelayCommand]
        private void NavigateToAlert()
        {
            var mainviewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            mainviewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AlertViewModel>();

        }
    }


}
