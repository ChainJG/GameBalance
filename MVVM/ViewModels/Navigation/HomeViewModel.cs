using GameBalance.Framework.Controls;
using GameBalance.Framework.Core;
using GameBalance.Helper;
using GameBalance.Styles.InfoCard;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

namespace GameBalance.MVVM.ViewModels.Navigation
{
    public sealed class HomeViewModel : IDisposable
    {
        public ObservableCollection<InfoCardViewModel> HardwareCards { get; } = [];

        public HomeViewModel()
        {
            GameBalanceUI.StartupCompleted += OnStartupCompleted;
        }
        public void Dispose()
        {
            GameBalanceUI.StartupCompleted -= OnStartupCompleted;
        }

        private void OnStartupCompleted()
        {
            BuildHardwareCards();
        }

        #region Hardware Info Cards
        private void BuildHardwareCards()
        {
            var systemInfo = GameBalanceContext.SystemInfo;

            HardwareCards.Clear();

            HardwareCards.Add(CreateHardwareCard(
                PackIconKind.Cpu64Bit,
                "Processor",
                systemInfo?.CPU?.Name,
                systemInfo?.CPU?.CurrentClockSpeed));

            HardwareCards.Add(CreateHardwareCard(
                PackIconKind.Monitor,
                "Graphics",
                systemInfo?.GPU?.Name,
                systemInfo?.GPU?.AdapterRAM));

            HardwareCards.Add(CreateHardwareCard(
                PackIconKind.Memory,
                "Installed RAM",
                MathHelper.FormatBytes(systemInfo?.Memory?.TotalPhysicalMemory),
                systemInfo?.Memory?.PhysicalMemoryUsageText));

            HardwareCards.Add(CreateHardwareCard(
                PackIconKind.Storage,
                "Storage",
                systemInfo?.Storage?.TotalStorage,
                $"{systemInfo?.Storage?.TotalUsage} of {systemInfo?.Storage?.TotalStorage} used"));

            HardwareCards.Add(CreateHardwareCard(
                PackIconKind.MicrosoftWindows,
                "Windows",
                systemInfo?.OS?.Name,
                $"Build {systemInfo?.OS?.BuildNumber}"));

            HardwareCards.Add(CreateHardwareCard(
                PackIconKind.Chip,
                "Motherboard",
                systemInfo?.Motherboard?.MotherboardDisplayName,
                $"BIOS {systemInfo?.Motherboard?.BIOSVersion}"));

        }
        private static InfoCardViewModel CreateHardwareCard(PackIconKind icon, string title, string body, string footer)
        {
            return new InfoCardViewModel
            {
                State = InfoCardState.Information,
                IsInteractive = false,
                TitleIcon = icon,
                TitleText = title,
                BodyText = body,
                FooterText = footer
            };
        }
        #endregion
    }
}
