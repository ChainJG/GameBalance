using GameBalance.Framework.Navigation.Core;
using GameBalance.Framework.Navigation.Dock;
using System.Windows;

namespace GameBalance.Framework.Controls
{
    public class DockItemControl : InteractiveCard
    {
        static DockItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DockItemControl),
                new FrameworkPropertyMetadata(typeof(DockItemControl)));
        }

        public DockSelectionManager? SelectionManager
        {
            get => (DockSelectionManager?)GetValue(SelectionManagerProperty);
            set => SetValue(SelectionManagerProperty, value);
        }

        public static readonly DependencyProperty SelectionManagerProperty =
            DependencyProperty.Register(
                nameof(SelectionManager),
                typeof(DockSelectionManager),
                typeof(DockItemControl));

        protected override void OnClick()
        {
            base.OnClick();

            if (DataContext is not DockEntry item)
                return;

            // Visual Selection
            if (item.CanSelect)
                SelectionManager?.Select(item);
        }
    }
}