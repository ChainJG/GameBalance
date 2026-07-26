using GameBalance.MVVM.SplashScreen;
using GameBalance.MVVM.Windows;
using GameBalance.Styles.Themes.Core;
using System.Windows;

namespace GameBalance
{
    public partial class App : Application
    {
        private ThemeService _themeService = new();
        CancellationTokenSource _cancellationTokenSource = new();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var token = _cancellationTokenSource.Token;

            _themeService.Initialise();

            #region Process Look up Window
            if (e.Args.Length >= 2 &&
                e.Args[0].Equals("--process-lookup", StringComparison.OrdinalIgnoreCase))
            {
                var selectedFilePath = e.Args[1];

                var processLookupWindow = new ProcessLookupWindow(selectedFilePath);
                processLookupWindow.Show();

                return;
            } 
            #endregion

            var mainWindow = new MainWindow();

            var splashWindow = new SplashScreenWindow();
            var splashViewModel = new SplashScreenViewModel();

            splashWindow.DataContext = splashViewModel;
            splashWindow.Show();

            splashViewModel.StartupCompleted += () =>
            {
                mainWindow.Show();
                splashWindow.Close();
            };

            await splashViewModel.InitialiseApplicationAsync(token);
        }
    }
}
