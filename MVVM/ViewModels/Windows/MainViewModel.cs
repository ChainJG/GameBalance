using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameBalance.Framework.Navigation.Core;
using GameBalance.Framework.Navigation.Dock;
using GameBalance.Framework.Navigation.Provider;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

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

        public ObservableCollection<ActionItem> DockItems { get; }

        private readonly NavigationService _navigationService;
        public DockSelectionManager DockSelection { get; } = new();

        public MainViewModel()
        {
            _navigationService = new NavigationService();

            DockItems = NavigationItemProvider.GetItems(NavigateCommand);

            _navigationService.ViewChanged += OnViewChanged;
            DockSelection.SelectionChanged += OnDockSelectionChanged;

            DockSelection.Select(DockItems, PageId.Home);
            Navigate(PageId.Home);
        }

        private void OnViewChanged(object view)
        {
            CurrentView = view;
        }
        private void OnDockSelectionChanged(ActionItem? item)
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
