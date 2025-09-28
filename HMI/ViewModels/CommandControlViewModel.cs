using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HMI.ViewModels
{
   public partial class CommandControlViewModel : ObservableObject
    {
        private IServiceProvider _serviceProvider;
        private  HttpClient _http;

        [ObservableProperty]
        private string _title = "Control Fan";

        [ObservableProperty]
        private double? _value;

        public CommandControlViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _http = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
        }

        [RelayCommand]
        public async Task TurnOn()
        {
            var command = new CommandControl { Action = "TurnOn" };
            await _http.PostAsJsonAsync("/Commands" , command);
        }

        [RelayCommand]
        public async Task TurnOff()
        {
            var command = new CommandControl { Action = "TurnOff" };
            await _http.PostAsJsonAsync("/Commands", command);
        }

        [RelayCommand]
        public async Task SetSpeed()
        {
            var command = new CommandControl { Action = "SetSpeed", Value = Value.Value };
            await _http.PostAsJsonAsync("/Commands", command);
        }



    }
}
