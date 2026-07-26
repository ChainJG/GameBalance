using GameBalance.Framework.Navigation.Core;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GameBalance.Framework.Navigation.Provider
{
    public static class NavigationItemProvider
    {
        public static ObservableCollection<ActionItem> GetItems(ICommand navigateCommand)
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


        private static ActionItem Create(string name, PackIconKind icon, PageId page, ICommand command)
        {
            return new ActionItem
            {
                Name = name,
                Icon = icon,
                Command = command,
                CommandParameter = page,
                CanSelect = true
            };
        }
    }

    public enum PageId
    {
        Home,
        Windows,
        System,
        Storage,
        Installer
    }
}