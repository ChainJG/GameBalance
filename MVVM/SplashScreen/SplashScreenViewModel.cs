using CommunityToolkit.Mvvm.ComponentModel;
using GameBalance.Core.Results;
using GameBalance.Diagnostics;
using GameBalance.Initialisation.Core;

namespace GameBalance.MVVM.SplashScreen
{
    public partial class SplashScreenViewModel : ObservableObject
    {
        private readonly StartupCoordinator _coordinator = new();

        #region Properties
        [ObservableProperty]
        public string statusText = "Initialising...";

        [ObservableProperty]
        public int progressPercentage = 0;
        #endregion

        public event Action? StartupCompleted;

        public async Task InitialiseApplicationAsync(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                var progress = new Progress<ProgressResult>(UpdateProgress);

                var result = await _coordinator.InitialiseAsync(progress, token);

                await Task.Delay(1000, token);
            }
            catch (Exception ex)
            {
                GameBalanceDebug.Error("Failed to initialise GameBalance", ex);
            }
            finally
            {
                CompleteStartupAsync();
            }
        }

        private void CompleteStartupAsync()
        {
            StartupCompleted?.Invoke();
        }

        private void UpdateProgress(ProgressResult result)
        {
            StatusText = result.Status;
            ProgressPercentage = result.Percent;
        }
    }
}
