using GameBalance.Framework.Navigation.Dock;
using System.Windows.Controls;
using System.Windows;

namespace GameBalance.Framework.Controls;

public class DockControl : ItemsControl
{
    static DockControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(DockControl),
            new FrameworkPropertyMetadata(typeof(DockControl)));
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
            typeof(DockControl));
}