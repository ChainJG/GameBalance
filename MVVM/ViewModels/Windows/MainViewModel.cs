using GameBalance.Framework.Navigation.Provider;
using GameBalance.Framework.Navigation.Core;
using GameBalance.Framework.Navigation.Dock;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using GameBalance.MVVM.Windows;
using System.Diagnostics;
using GameBalance.Framework.Notifications;
using GameBalance.Framework.Controls;

namespace GameBalance.MVVM.ViewModels.Windows
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? currentView;

        #region Page Properties
        [ObservableProperty]
        private string pageTitle;
        [ObservableProperty]
        private PackIconKind pageIcon; 
        #endregion

        public ObservableCollection<DockEntry> DockItems { get; }

        private readonly NavigationService _navigationService;
        public DockSelectionManager DockSelection { get; } = new();

        public MainViewModel()
        {
            _navigationService = new NavigationService();

            DockItems = NavigationItemProvider.GetItems(NavigateCommand);
            AddProcessLockUp();

            _navigationService.ViewChanged += OnViewChanged;
            DockSelection.SelectionChanged += OnDockSelectionChanged;

            DockSelection.Select(DockItems, PageId.Home);
            Navigate(PageId.Home);
        }

        private void AddProcessLockUp()
        {
            var processLockup = new DockEntry
            {
                Icon = PackIconKind.GraphicsProcessingUnit,
                Name = "Process",
                CanSelect = false,
                Command = new RelayCommand(() => new ProcessLookupWindow("").Show())
            };

            DockItems.Add(processLockup);
        }
        private void OnViewChanged(object view)
        {
            CurrentView = view;
        }
        private void OnDockSelectionChanged(DockEntry? item)
        {
            if (item is null)
                return;

            PageTitle = item.Name;
            PageIcon = item.Icon;
        }

        [RelayCommand]
        private void Navigate(PageId page)
        {
            _navigationService.Navigate(page);
        }
    }
}
