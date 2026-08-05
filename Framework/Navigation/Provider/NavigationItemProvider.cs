using GameBalance.Framework.Navigation.Core;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GameBalance.Framework.Navigation.Provider
{
    public enum PageId
    {
        Home,
        Windows,
        System,
        Storage,
        Installer
    }

    public static class NavigationItemProvider
    {
        public static ObservableCollection<DockEntry> GetItems(ICommand navigateCommand)
        {
            return
            [
                Create(
                    "Home",
                    PackIconKind.Home,
                    PageId.Home,
                    navigateCommand),

                Create(
                    "Windows",
                    PackIconKind.MicrosoftWindows,
                    PageId.Windows,
                    navigateCommand),

                Create(
                    "System",
                    PackIconKind.Computer,
                    PageId.System,
                    navigateCommand),

                Create(
                    "Storage",
                    PackIconKind.Storage,
                    PageId.System,
                    navigateCommand),

                Create(
                    "Installer",
                    PackIconKind.AppsBox,
                    PageId.Installer,
                    navigateCommand),
            ];
        }

        private static DockEntry Create(string name, PackIconKind icon, PageId page, ICommand command) =>
            new()
            {
                Name = name,
                Icon = icon,
                Command = command,
                CommandParameter = page,
                CanSelect = true
            };
    }
}