using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameBalance.Framework.Navigation.Core;
using GameBalance.Framework.Navigation.Dock;
using GameBalance.Framework.Navigation.Provider;
using System.Collections.ObjectModel;

namespace GameBalance.MVVM.ViewModels.Windows
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? currentView;
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
        }

        [RelayCommand]
        private void Navigate(PageId page)
        {
            _navigationService.Navigate(page);
        }
    }
}
