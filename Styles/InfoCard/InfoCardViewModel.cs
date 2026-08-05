using CommunityToolkit.Mvvm.ComponentModel;
using GameBalance.Framework.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace GameBalance.Styles.InfoCard
{
    public partial class InfoCardViewModel : ObservableObject
    {
        [ObservableProperty]
        public string titleText = string.Empty;
        [ObservableProperty]
        public PackIconKind titleIcon = PackIconKind.QuestionAnswer;
        [ObservableProperty]
        public string bodyText = string.Empty;
        [ObservableProperty]
        private string footerText = string.Empty;
        [ObservableProperty]
        public bool isInteractive = true;
        [ObservableProperty]
        private InfoCardState state = InfoCardState.Information;
        public ICommand? ActionCommand { get; set; }
    }
}
