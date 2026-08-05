using GameBalance.Framework.Notifications;
using System.Windows;

namespace GameBalance.Framework.Controls
{
    public class NotificationBannerControl : InfoCardControl
    {
        static NotificationBannerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(NotificationBannerControl),
                new FrameworkPropertyMetadata(typeof(NotificationBannerControl)));
        }

        public NotificationItem? NotificationItem
        {
            get => (NotificationItem?)GetValue(NotificationItemProperty);
            set => SetValue(NotificationItemProperty, value);
        }

        public static readonly DependencyProperty NotificationItemProperty =
            DependencyProperty.Register(
                nameof(NotificationItem),
                typeof(NotificationItem),
                typeof(NotificationBannerControl),
                new PropertyMetadata(null, OnNotificationChanged));

        private static void OnNotificationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotificationBannerControl banner)
            {
                banner.UpdateNotification();
            }
        }

        private void UpdateNotification()
        {
            if (NotificationItem is null)
                return;

            InfoState = NotificationItem.State;
        }

        protected override void OnClick()
        {
            base.OnClick();

            ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            if (NotificationItem is null)
                return;

            NotificationService.Remove(NotificationItem);
        }
    }
}
