using CommunityToolkit.Mvvm.ComponentModel;

namespace GameBalance.Framework.Notifications
{
    public sealed partial class NotificationState : ObservableObject
    {
        [ObservableProperty]
        private NotificationItem? currentNotification;
    }
}
