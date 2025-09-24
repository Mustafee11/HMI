using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HMI.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace HMI.ViewModels
{
    public partial class AlertViewModel : ObservableObject
    {
        private IServiceProvider _serviceProvider;
        private HubConnection? _hub;
        private bool _started;

        public AlertViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [ObservableProperty]
        private string _title = "Alerts";

        [ObservableProperty]
        private ObservableCollection<Alert> _alertsItems = [];

        [RelayCommand]
        private void NavigateToHome() { 
        
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

            _hub.On<Alert>("Alert", (alert) =>
            {
                App.Current.Dispatcher.Invoke(() => AlertsItems.Insert(0, alert));

            });


            if (_hub != null)
                await _hub!.StartAsync();
        }
        public async Task StopAsync()
        {
          if(_hub is null) return;
            try
            {
                await _hub.StopAsync();
            }
            finally
            {
                await _hub.DisposeAsync();
                _hub = null;
                _started= false;
            }
        }

        public async ValueTask DisposeAsync() => await StopAsync();



    }


}
