using GameBalance.Framework.Controls;

namespace GameBalance.Framework.Notifications;

public static class NotificationService
{
    public static NotificationState State { get; } = new();
    private static readonly List<NotificationItem> _notificationQueue = [];

    public static void Show(NotificationItem notification)
    {
        if (notification is null)
            return;

        _notificationQueue.Add(notification);

        ProcessQueue();
    }

    public static void Remove(NotificationItem notification)
    {
        _notificationQueue.Remove(notification);

        if (ReferenceEquals(State.CurrentNotification, notification))
        {
            State.CurrentNotification = null;
            ProcessQueue();
        }
    }

    public static void CloseCurrent()
    {
        if (State.CurrentNotification is null)
            return;

        Remove(State.CurrentNotification);
    }

    private static void ProcessQueue()
    {
        NotificationItem? nextNotification = _notificationQueue
            .OrderBy(x => x.Priority)
            .ThenBy(x => GetTypePriority(x.State))
            .FirstOrDefault();

        if (nextNotification is null)
            return;

        State.CurrentNotification = nextNotification;

        if (!nextNotification.RequiresManualClose)
        {
            _ = RemoveAfterDelay(nextNotification);
        }
    }

    private static async Task RemoveAfterDelay(NotificationItem notification)
    {
        await Task.Delay(notification.Duration);

        // Prevent an expired notification from removing a newer one
        if (ReferenceEquals(State.CurrentNotification, notification))
        {
            Remove(notification);
        }
    }

    private static int GetTypePriority(InfoCardState state)
    {
        return state switch
        {
            InfoCardState.Caution => 0,
            InfoCardState.Error => 1,
            InfoCardState.Warning => 2,
            InfoCardState.Information => 3,
            InfoCardState.Successful => 4,

            _ => 99
        };
    }
}