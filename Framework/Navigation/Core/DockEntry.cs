using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace GameBalance.Framework.Navigation.Core
{
    public partial class DockEntry : ObservableObject
    {
        public required string Name { get; init; }

        public required PackIconKind Icon { get; init; }

        public ICommand? Command { get; init; }

        public object? CommandParameter { get; init; }

        [ObservableProperty]
        private bool isSelected;

        public bool CanSelect { get; init; } = true;
    }
}
