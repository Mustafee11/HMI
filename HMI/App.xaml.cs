using HMI.ViewModels;
using HMI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Windows;

namespace HMI
{

    public partial class App : Application
    {
        private IHost? _host;

        public App()
        {
            var bulider = Host.CreateApplicationBuilder();

          
            bulider.Services.AddSingleton<AlertViewModel>();
           
            bulider.Services.AddTransient<AlertView>();
            
            bulider.Services.AddTransient<HomeViewModel>();
           
            bulider.Services.AddTransient<HomeView>();


            bulider.Services.AddSingleton<FanStatusViewModel>();

            bulider.Services.AddTransient<FanStatusView>();

            bulider.Services.AddSingleton<MainViewModel>();
            bulider.Services.AddSingleton<MainWindow>();


            _host = bulider.Build();
               
            
        }

        protected override void OnStartup ( StartupEventArgs e)
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("/HMI;component/Resources/Styling.xaml", UriKind.Relative)
            });

            var alertViewModel  = _host!.Services.GetRequiredService<AlertViewModel>();
            _ = alertViewModel.StartAsync();

            var FanStatusViewModel = _host!.Services.GetRequiredService<FanStatusViewModel>();
            _ = FanStatusViewModel.StartAsync();

            var mainWindow = _host!.Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _host!.Services.GetRequiredService<MainViewModel>();
            mainWindow.Show();

        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose();
            base.OnExit(e);
        }

    }

   

    

}
