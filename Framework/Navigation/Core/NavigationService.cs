using GameBalance.Framework.Navigation.Provider;
using GameBalance.MVVM.ViewModels.Navigation;

namespace GameBalance.Framework.Navigation.Core
{
    public sealed class NavigationService
    {
        public event Action<object>? ViewChanged;

        public void Navigate(PageId page)
        {
            object viewModel = page switch
            {
                PageId.Home => new HomeViewModel(),
                PageId.Windows => new WindowsViewModel(),
                PageId.System => new SystemViewModel(),
                PageId.Storage => new StorageViewModel(),
                PageId.Installer => new InstallerViewModel(),

                _ => throw new ArgumentOutOfRangeException(nameof(page))
            };

            ViewChanged?.Invoke(viewModel);
        }
    }
}
