using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HMI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMI.ViewModels
{
   public partial class FanStatusViewModel : ObservableObject
    {
        private IServiceProvider _serviceProvider;
        private HubConnection? _hub;
        private bool _started;

        [ObservableProperty]
        private string _title ="Fanstatus";

        [ObservableProperty]
        private ObservableCollection<Fanstatus> _fanitems = [];

        public FanStatusViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        public void NavigateToHome()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
        }

        public async Task StartAsync()
        {
            if (_started) return;
            _started = true;

            var clientId = $"hmi-{Environment.MachineName}";

            _hub = new HubConnectionBuilder()
                .WithUrl($"http://localhost:5000/hmi?clientId={clientId}")
                .WithAutomaticReconnect()
                .Build();

            _hub.On<Fanstatus>("FanStatusUpdate", (fanstatus) =>
            {
                App.Current.Dispatcher.Invoke(() => Fanitems.Insert(0, fanstatus));
            });


            if (_hub != null)
                await _hub!.StartAsync();
        }

        public async Task StopAsync()
        {
            if (_hub is null) return;
            try
            {
                await _hub.StopAsync();
            }
            finally
            {
                await _hub.DisposeAsync();
                _hub = null;
                _started = false;
            }
        }

        public async ValueTask DisposeAsync() => await StopAsync();
    }
}
