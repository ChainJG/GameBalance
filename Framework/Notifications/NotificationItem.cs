using CommunityToolkit.Mvvm.ComponentModel;
using GameBalance.Framework.Controls;

namespace GameBalance.Framework.Notifications
{
    public enum NotificationPriority
    {
        High,
        Medium,
        Low
    }
    public partial class NotificationItem : ObservableObject
    {
        public string DisplayTitle => $"{State}: {Title}";

        public required string Title { get; init; } = string.Empty;

        public required string Message { get; init; } = string.Empty;

        public NotificationPriority Priority { get; init; } = NotificationPriority.Low;

        [ObservableProperty]
        private InfoCardState state = InfoCardState.Successful;

        public bool RequiresManualClose { get; init; } = false;

        public TimeSpan Duration { get; init; } = TimeSpan.FromSeconds(4);

    }
}
