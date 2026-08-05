using GameBalance.Framework.Navigation.Provider;
using GameBalance.MVVM.ViewModels.Navigation;

namespace GameBalance.Framework.Navigation.Core
{
    public sealed class NavigationService
    {
        public event Action<object>? ViewChanged;

        private readonly HomeViewModel _homeViewModel = new();
        private readonly WindowsViewModel _windowsViewModel = new();
        private readonly SystemViewModel _systemViewModel = new();
        private readonly StorageViewModel _storageViewModel = new();
        private readonly InstallerViewModel _installerViewModel = new();

        public void Navigate(PageId page)
        {
            object viewModel = page switch
            {
                PageId.Home => _homeViewModel,
                PageId.Windows => _windowsViewModel,
                PageId.System => _systemViewModel,
                PageId.Storage => _storageViewModel,
                PageId.Installer => _installerViewModel,
                _ => throw new ArgumentOutOfRangeException(nameof(page))
            };

            ViewChanged?.Invoke(viewModel);
        }
    }
}
